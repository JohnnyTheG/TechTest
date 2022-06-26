using System.Text.RegularExpressions;

namespace TechTest
{
    public static class DateTimeOperation
    {
        /// <summary>
        /// Used validate format and to extract the operator, unit count and unit type from operation strings.
        /// </summary>
        private static Regex parseOperationRegex = new Regex("(now\\(\\))(?<operator>[+-])(?<count>\\d+)(?<unit>(mon\\.?|[smhdy]))", RegexOptions.IgnoreCase);

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

            public UnitDefinitions UnitDefinition
            {
                get
                {
                    switch (Unit.ToLower())
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

            public string Operator { get; set; }

            public int Count { get; set; }

            public string Unit { get; set; }
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
                Unit = operationMatch.Groups["unit"].Value
            };

            return parseOperationResult;
        }

        public static UtcComponents ExecuteUtcNow(string operation)
        {
            UtcComponents utcNowComponents = UtcUtils.UtcNowComponents;

            return Execute(operation, utcNowComponents);
        }

        public static UtcComponents Execute(string operation, UtcComponents utcComponents)
        {
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

                    utcComponents.Add(parseOperationResult.UnitDefinition, parseOperationResult.Count);

                    break;

                case OperatorDefinitions.Subtract:

                    utcComponents.Subtract(parseOperationResult.UnitDefinition, parseOperationResult.Count);

                    break;

                default:

                    // TODO: New exception for unsupported operation type?
                    throw new InvalidOperationException();
            }

            // TODO: Validate results and roll values round if needed.

            return utcComponents;
        }

        internal static void DoInternal()
        {
        }
    }
}