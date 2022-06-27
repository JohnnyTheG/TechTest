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
        private static Regex parseOperationRegex = new Regex("(now\\(\\))(?<operator>[+-])(?<count>\\d+)(?<unit>(mon\\.?|[smhdy]))(@)?(?<snap>((mon\\.?|[smhdy])?))", RegexOptions.IgnoreCase);

        public class InvalidOperationException : Exception { }

        private struct ParseOperationResult
        {
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
            
            public string Operator { get; set; }

            public int Count { get; set; }

            public string Unit { get; set; }

            public string Snap { get; set; }

            public bool ShouldSnap { get { return Snap != string.Empty; } }

            public UnitDefinitions UnitDefinition { get { return ParseUnitDefinition(Unit); } }

            public UnitDefinitions SnapUnitDefinition { get { return ParseUnitDefinition(Snap); } }

            private UnitDefinitions ParseUnitDefinition(string unit)
            {
                switch (unit.ToLower())
                {
                    case "y":

                        return UnitDefinitions.Years;

                    case "mon":

                        return UnitDefinitions.Months;

                    case "d":

                        return UnitDefinitions.Days;

                    case "h":

                        return UnitDefinitions.Hours;

                    case "m":

                        return UnitDefinitions.Minutes;

                    case "s":

                        return UnitDefinitions.Seconds;

                    default:

                        return UnitDefinitions.Undefined;
                }
            }
        }

        private static ParseOperationResult ParseOperation(string operation)
        {
            Match operationMatch = parseOperationRegex.Match(operation);

            if (!operationMatch.Success)
            {
                throw new InvalidOperationException();
            }

            ParseOperationResult parseOperationResult = new ParseOperationResult
            {
                Operator = operationMatch.Groups["operator"].Value,
                Count = int.Parse(operationMatch.Groups["count"].Value),
                Unit = operationMatch.Groups["unit"].Value,
                Snap = operationMatch.Groups["snap"].Value,
            };

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

            switch (parseOperationResult.OperatorDefintion)
            {
                case OperatorDefinitions.Add:

                    returnUtcComponents.Add(parseOperationResult.UnitDefinition, parseOperationResult.Count);

                    break;

                case OperatorDefinitions.Subtract:

                    returnUtcComponents.Subtract(parseOperationResult.UnitDefinition, parseOperationResult.Count);

                    break;

                default:

                    // TODO: New exception for unsupported operation type?
                    throw new InvalidOperationException();
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