namespace ConsoleApp;
class ConsoleWrapper : IConsole
{
    public string? ReadLine()
        => Console.ReadLine();

    public void WriteLine(string text)
        => Console.WriteLine(text);

    public void SetForegroundColor(ConsoleColor color)
    {
        if (Console.IsOutputRedirected)
            return;

        Console.ForegroundColor = color;
    }

    public void ResetColor()
    {
        if (Console.IsOutputRedirected)
            return;

        Console.ResetColor();
    }

    public void SetTopPosition(int change)
    {
        if (Console.IsOutputRedirected)
            return;

        Console.SetCursorPosition(0, Console.CursorTop + change);
    }
}

interface IConsole
{
    string? ReadLine();
    void WriteLine(string text);
    void SetForegroundColor(ConsoleColor color);
    void ResetColor();
    void SetTopPosition(int change);
}