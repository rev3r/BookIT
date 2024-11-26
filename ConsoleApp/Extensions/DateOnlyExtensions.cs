using System.Globalization;

namespace ConsoleApp;
static class DateOnlyExtensions
{
    public static string ToFormattedString(this DateOnly date)
        => date.ToString(DateFormat, CultureInfo.InvariantCulture);
}
