using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("TechTestTests")]

namespace TechTest
{
    public static class DateTimeOperation
    {
        /// <summary>
        /// Used validate format and extract the operations and snap settings.
        /// </summary>
        private static Regex parseAllComponentsRegex = new Regex("(now\\(\\))((?<operations>[+-]\\d+(mon\\.?|[smhdy]))+)?(@)?(?<snap>((mon\\.?|[smhdy])))?", RegexOptions.IgnoreCase);

        /// <summary>
        /// Used to extract the individual operations and their components from the list of operations obtained using the parseAllComponentsRegex call.
        /// </summary>
        private static Regex parseOperationRegex = new Regex("(?<operator>[+-])(?<count>\\d+)(?<unit>(mon\\.?|[smhdy]))", RegexOptions.IgnoreCase);

        public class InvalidOperationException : Exception { }

        /// <summary>
        /// Contains the results of attempting to parse a date time operation from a string.
        /// </summary>
        private struct ParseOperationResult
        {
            /// <summary>
            /// Whether there are operations to apply.
            /// </summary>
            public bool ShouldPerformOperation { get { return Operations.Count > 0; } }

            /// <summary>
            /// Represents a single operation.
            /// </summary>
            public struct Operation
            {
                /// <summary>
                /// The operator to apply.
                /// </summary>
                public string Operator { get; set; }

                /// <summary>
                /// The number of units.
                /// </summary>
                public int Count { get; set; }

                /// <summary>
                /// The type of units.
                /// </summary>
                public string Unit { get; set; }

                /// <summary>
                /// Enumerated value for the type of operation.
                /// </summary>
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

                /// <summary>
                /// Enumerated value for the type of unit.
                /// </summary>
                public UnitDefinitions UnitDefinition { get { return DateTimeOperationUtils.ParseUnitDefinition(Unit); } }
            }

            /// <summary>
            /// The operations to apply.
            /// </summary>
            public List<Operation> Operations { get; set; }

            /// <summary>
            /// The units to snap to.
            /// </summary>
            public string Snap { get; set; }

            /// <summary>
            /// Whether the operation should apply a snap.
            /// </summary>
            public bool ShouldSnap { get { return !string.IsNullOrEmpty(Snap); } }

            /// <summary>
            /// Enumerated value for the unit to snap to.
            /// </summary>
            public UnitDefinitions SnapUnitDefinition { get { return DateTimeOperationUtils.ParseUnitDefinition(Snap); } }
        }

        /// <summary>
        /// Parse an operation from a string containing any number of operations.
        /// </summary>
        /// <param name="allComponents"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private static ParseOperationResult ParseOperation(string allComponents)
        {
            Match allComponentsMatch = parseAllComponentsRegex.Match(allComponents);

            if (!allComponentsMatch.Success)
            {
                throw new InvalidOperationException();
            }

            // Create the empty list of operations and the snap setting.
            ParseOperationResult parseOperationResult = new ParseOperationResult
            {
                Operations = new List<ParseOperationResult.Operation>(),
                Snap = allComponentsMatch.Groups["snap"].Value,
            };

            // For each operation captures, parse the components of that operation (operator, count and unit type).
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
                    Count = int.Parse(operationMatch.Groups["count"].Value),
                    Unit = operationMatch.Groups["unit"].Value,
                };

                parseOperationResult.Operations.Add(operation);
            }

            return parseOperationResult;
        }

        /// <summary>
        /// Perform the operation specified in the string parameter using the current UTC time.
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static string Execute(string operation)
        {
            UtcComponents utcNowComponents = UtcUtils.UtcNowComponents;

            UtcComponents resultUtcComponents = Execute(operation, utcNowComponents);

            return resultUtcComponents.ToString();
        }

        /// <summary>
        /// Perform the operation specified in the string parameter using the utc components passed as an argument.
        /// This is currently internal and only directly used for testing.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="utcComponents"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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

                            throw new InvalidOperationException();
                    }
                }
            }

            if (parseOperationResult.ShouldSnap)
            {
                UnitDefinitions snapUnitDefinition = parseOperationResult.SnapUnitDefinition;

                // Get the enum values which are lower than the snap (so hours would have minutes, seconds and milliseconds below it).
                IEnumerable<UnitDefinitions> snappedUnitDefinitions = new List<UnitDefinitions>(Enum.GetValues(typeof(UnitDefinitions)).Cast<UnitDefinitions>()).Where(unitDefinition => unitDefinition != UnitDefinitions.Undefined && (int)unitDefinition < (int)snapUnitDefinition);

                returnUtcComponents.Snap(snappedUnitDefinitions);
            }

            return returnUtcComponents;
        }
    }
}