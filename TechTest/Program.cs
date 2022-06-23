string test = "now()-6Y";

try
{
    TechTest.ParseOperationResult parseOperationResult = TechTest.ParseOperation(test);

    Console.WriteLine($"Operator: {parseOperationResult.Operator}");
    Console.WriteLine($"Count: {parseOperationResult.Count}");
    Console.WriteLine($"Unit: {parseOperationResult.Unit}");
}
catch (TechTest.InvalidOperationException invalidOperationException)
{
    Console.WriteLine("Invalid Operation");

    throw invalidOperationException;
}