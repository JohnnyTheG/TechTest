using TechTest;

string testOperation = "now()-6Y";

try
{
    string result = DateTimeOperation.Execute(testOperation).ToString();

    Console.WriteLine(result);
}
catch (DateTimeOperation.InvalidOperationException invalidOperationException)
{
    Console.WriteLine("Invalid Operation");

    throw invalidOperationException;
}