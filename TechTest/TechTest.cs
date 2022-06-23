using System.Text.RegularExpressions;

public static class TechTest
{
    /// <summary>
    /// Used to ensure that the operation string is in a supported format.
    /// </summary>
    //private static Regex validateFormatRegex = new Regex("(now\\(\\))[+-]\\d+(mon\\.?|[smhdy])", RegexOptions.IgnoreCase);

    /// <summary>
    /// Used validate format and to extract the operator, unit count and unit type from operation strings.
    /// </summary>
    private static Regex parseOperationRegex = new Regex("(now\\(\\))(?<operator>[+-])(?<count>\\d+)(?<unit>(mon\\.?|[smhdy]))", RegexOptions.IgnoreCase);

    public class InvalidOperationException : Exception { }

    public struct ParseOperationResult
    {
        public string Operator { get; set; }

        public string Count { get; set; }

        public string Unit { get; set; }
    }

    public static ParseOperationResult ParseOperation(string operation)
    {
        Match operationMatch = parseOperationRegex.Match(operation);

        if (!operationMatch.Success)
        {
            throw new InvalidOperationException();
        }

        ParseOperationResult parseOperationResult = new ParseOperationResult
        {
            Operator = operationMatch.Groups["operator"].Value,
            Count = operationMatch.Groups["count"].Value,
            Unit = operationMatch.Groups["unit"].Value
        };

        return parseOperationResult;
    }

    public static string Execute(string operation)
    {
        try
        {
            ParseOperationResult parseOperationResult = ParseOperation(operation);
        }
        catch (InvalidOperationException invalidOperationException)
        {
            throw invalidOperationException;
        }

        return string.Empty;
    }
}