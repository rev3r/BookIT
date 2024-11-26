namespace ConsoleApp;
static class IConsoleExtensions
{
    public static void WriteInfoLine(this IConsole console, string text)
        => console.WriteColoredLine(text, ConsoleColor.DarkGreen);

    public static void WriteErrorLine(this IConsole console, string text)
        => console.WriteColoredLine(text, ConsoleColor.Red, ErrorLogPrefix);

    private static void WriteColoredLine(this IConsole console, string text, ConsoleColor color, string prefix = "")
    {
        console.SetForegroundColor(color);
        console.WriteLine(prefix + text);
        console.ResetColor();
    }
}
