namespace IntegrationTests;
public class FakeConsole : IConsole
{
    private string _input = "";

    public List<string> Outputs { get; } = [];

    public void SetupInput(string input)
        => _input = input;

    public string? ReadLine()
    {
        var line = _input + Environment.NewLine;
        _input = ""; // Prevents multiple reads

        return line;
    }

    public void WriteLine(string text)
        => Outputs.Add(text);

    public void SetForegroundColor(ConsoleColor color)
    { }

    public void ResetColor()
    { }

    public void SetTopPosition(int change)
    { }
}
