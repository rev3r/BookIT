namespace IntegrationTests;
static class FakeConsoleAsserts
{
    public static void AssertOutput(this FakeConsole console, string expectedLine)
    {
        var line = Assert.Single(console.Outputs);
        Assert.Equal(expectedLine, line);
    }
}
