namespace IntegrationTests;
static class ConsoleHelper
{
    public static void SetupIn(string input)
        => SetupInExact(input + Environment.NewLine);

    public static void SetupInExact(string input)
    {
        var @in = new StringReader(input);
        Console.SetIn(@in);
    }

    public static StringWriter SetupOut()
    {
        var @out = new StringWriter();
        Console.SetOut(@out);

        return @out;
    }
}
