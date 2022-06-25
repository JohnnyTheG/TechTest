string test = "now()-6Y";

try
{
}
catch (TechTest.InvalidOperationException invalidOperationException)
{
    Console.WriteLine("Invalid Operation");

    throw invalidOperationException;
}