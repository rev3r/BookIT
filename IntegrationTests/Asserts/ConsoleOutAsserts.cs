namespace IntegrationTests;
static class ConsoleOutAsserts
{
    public static void AssertLogs(this StringWriter @out, string expectedLines)
        => AssertLogs(@out, expectedLines.Split(Environment.NewLine).ToArray());

    public static void AssertLogs(this StringWriter @out, string[] expectedLines)
    {
        var lines = GetLines(@out);

        Assert.Equal(expectedLines.Length, lines.Length);
        for (int i = 0; i < lines.Length; i++)
        {
            Assert.Equal(expectedLines[i], lines[i]);
        }
    }

    public static void AssertErrorLog(this StringWriter @out)
    {
        var line = Assert.Single(GetLines(@out));
        Assert.StartsWith(Constants.ErrorLogPrefix, line);
    }

    public static void AssertNoErrorLog(this StringWriter @out)
    {
        var output = @out.ToString();
        Assert.DoesNotContain(Constants.ErrorLogPrefix, output);
    }

    public static void AssertNoLogs(this StringWriter @out)
    {
        var output = @out.ToString();
        Assert.Empty(output);
    }

    private static string[] GetLines(StringWriter @out)
    {
        var output = @out.ToString();
        return output.Split(Environment.NewLine)[..^1];
    }
}
