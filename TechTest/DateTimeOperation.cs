using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("TechTestTests")]

namespace TechTest
{
    public static class DateTimeOperation
    {
        /// <summary>
        /// Used validate format and to extract the operator, unit count and unit type from operation strings.
        /// </summary>
        //private static Regex parseOperationRegex = new Regex("(now\\(\\))((?<operations>(?<operator>[+-])(?<count>\\d+)(?<unit>(mon\\.?|[smhdy])))+)?(@)?(?<snap>((mon\\.?|[smhdy])))?", RegexOptions.IgnoreCase);
        private static Regex parseAllComponentsRegex = new Regex("(now\\(\\))((?<operations>[+-]\\d+(mon\\.?|[smhdy]))+)?(@)?(?<snap>((mon\\.?|[smhdy])))?", RegexOptions.IgnoreCase);
        private static Regex parseOperationRegex = new Regex("(?<operator>[+-])(?<count>\\d+)(?<unit>(mon\\.?|[smhdy]))", RegexOptions.IgnoreCase);

        public class InvalidOperationException : Exception { }

        private struct ParseOperationResult
        {
            public bool ShouldPerformOperation { get { return Operations.Count > 0; } }

            public struct Operation
            {
                public string Operator { get; set; }

                public int Count { get; set; }

                public string Unit { get; set; }

                public OperatorDefinitions OperatorDefintion
                {
                    get
                    {
                        switch (Operator)
                        {
                            case "+":

                                return OperatorDefinitions.Add;

                            case "-":

                                return OperatorDefinitions.Subtract;

                            default:

                                return OperatorDefinitions.Undefined;
                        }
                    }
                }

                public UnitDefinitions UnitDefinition { get { return DateTimeOperationUtils.ParseUnitDefinition(Unit); } }
            }

            //public string Operator { get; set; }

            //public int Count { get; set; }

            //public string Unit { get; set; }

            public List<Operation> Operations { get; set; }

            public string Snap { get; set; }

            public bool ShouldSnap { get { return !string.IsNullOrEmpty(Snap); } }

            public UnitDefinitions SnapUnitDefinition { get { return DateTimeOperationUtils.ParseUnitDefinition(Snap); } }
        }

        private static ParseOperationResult ParseOperation(string allComponents)
        {
            Match allComponentsMatch = parseAllComponentsRegex.Match(allComponents);

            if (!allComponentsMatch.Success)
            {
                throw new InvalidOperationException();
            }

            ParseOperationResult parseOperationResult = new ParseOperationResult
            {
                //Operator = operationMatch.Groups["operator"].Value,
                //Count = string.IsNullOrEmpty(operationMatch.Groups["count"].Value) ? 0 : int.Parse(operationMatch.Groups["count"].Value),
                //Unit = operationMatch.Groups["unit"].Value,
                Operations = new List<ParseOperationResult.Operation>(),
                Snap = allComponentsMatch.Groups["snap"].Value,
            };

            foreach (Capture capture in allComponentsMatch.Groups["operations"].Captures)
            {
                Match operationMatch = parseOperationRegex.Match(capture.Value);

                if (!operationMatch.Success)
                {
                    throw new InvalidOperationException();
                }

                ParseOperationResult.Operation operation = new ParseOperationResult.Operation()
                {
                    Operator = operationMatch.Groups["operator"].Value,
                    Count = string.IsNullOrEmpty(operationMatch.Groups["count"].Value) ? 0 : int.Parse(operationMatch.Groups["count"].Value),
                    Unit = operationMatch.Groups["unit"].Value,
                };

                parseOperationResult.Operations.Add(operation);
            }

            return parseOperationResult;
        }

        public static string Execute(string operation)
        {
            UtcComponents utcNowComponents = UtcUtils.UtcNowComponents;

            UtcComponents resultUtcComponents = Execute(operation, utcNowComponents);

            return resultUtcComponents.ToString();
        }

        internal static UtcComponents Execute(string operation, UtcComponents utcComponents)
        {
            // Clone to prevent altering passed utc components.
            UtcComponents returnUtcComponents = (UtcComponents)utcComponents.Clone();

            ParseOperationResult parseOperationResult;

            try
            {
                parseOperationResult = ParseOperation(operation);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                throw invalidOperationException;
            }

            if (parseOperationResult.ShouldPerformOperation)
            {
                foreach (ParseOperationResult.Operation parsedOperation in parseOperationResult.Operations)
                {
                    switch (parsedOperation.OperatorDefintion)
                    {
                        case OperatorDefinitions.Add:

                            returnUtcComponents.Add(parsedOperation.UnitDefinition, parsedOperation.Count);

                            break;

                        case OperatorDefinitions.Subtract:

                            returnUtcComponents.Subtract(parsedOperation.UnitDefinition, parsedOperation.Count);

                            break;

                        default:

                            // TODO: New exception for unsupported operation type?
                            throw new InvalidOperationException();
                    }
                }
            }

            if (parseOperationResult.ShouldSnap)
            {
                UnitDefinitions snapUnitDefinition = parseOperationResult.SnapUnitDefinition;

                IEnumerable<UnitDefinitions> snappedUnitDefinitions = new List<UnitDefinitions>(Enum.GetValues(typeof(UnitDefinitions)).Cast<UnitDefinitions>()).Where(unitDefinition => unitDefinition != UnitDefinitions.Undefined && (int)unitDefinition < (int)snapUnitDefinition);

                returnUtcComponents.Snap(snappedUnitDefinitions);
            }

            return returnUtcComponents;
        }
    }
}