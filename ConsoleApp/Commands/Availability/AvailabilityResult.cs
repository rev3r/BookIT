using static System.Globalization.CultureInfo;

namespace ConsoleApp;
record AvailabilityResult(int Value) : IOutputableResult
{
    public static AvailabilityResult Empty { get; } = new(0);

    public string ToOutputString()
        => Value.ToString(InvariantCulture);
}