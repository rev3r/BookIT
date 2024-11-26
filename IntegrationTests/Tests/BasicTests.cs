namespace IntegrationTests;
public class BasicTests
{
    public static readonly TheoryData<string[]> InvalidShapeArgs =
    [
        [],
        ["--hotels", "hotels.json"],
        ["--bookings", "bookings.json"],
        ["--hotels", "--bookings"],
        ["hotels.json", "bookings.json"],
        ["-hotels", "hotels.json", "-bookings", "bookings.json"],
        ["hotels", "hotels.json", "bookings", "bookings.json"],
    ];

    [Theory]
    [MemberData(nameof(InvalidShapeArgs))]
    public void InvalidShapeArgs_ErrorLog(string[] args)
    {
        // Arrange
        using var @out = ConsoleHelper.SetupOut();

        // Act
        InvokationHelper.RunProgram(args);

        // Assert
        @out.AssertErrorLog();
    }

    public static readonly TheoryData<string[]> ValidArgs =
    [
        ["--hotels", "hotels.json", "--bookings", "bookings.json"],
        ["--bookings", "bookings.json", "--hotels", "hotels.json"],
        ["--hotels", "hotels.json", "--bookings", "bookings.json", "--foo", "foo.json"],
        ["--foo", "foo.json", "--hotels", "hotels.json", "--bookings", "bookings.json"],
        ["--hotels", "hotels.json", "--foo", "foo.json", "--bookings", "bookings.json"],
        ["--hotels", "hotels.json", "foo", "--bookings", "bookings.json"],
    ];

    [Theory]
    [MemberData(nameof(ValidArgs))]
    public void ValidShapeArgs_NoLog(string[] args)
    {
        // Arrange
        ConsoleHelper.SetupIn("");
        using var @out = ConsoleHelper.SetupOut();

        // Act
        InvokationHelper.RunProgram(args);

        // Assert
        @out.AssertNoLogs();
    }


    public static readonly TheoryData<string> EmptyInput =
    [
        string.Empty,
        Environment.NewLine,
    ];

    [Theory]
    [MemberData(nameof(EmptyInput))]
    public void EmptyInput_NoLog(string input)
    {
        // Arrange
        ConsoleHelper.SetupInExact(input);
        using var @out = ConsoleHelper.SetupOut();

        // Act
        InvokationHelper.RunProgram(TestConstants.Args);

        // Assert
        @out.AssertNoLogs();
    }

    public static readonly TheoryData<string, string> ValidInput = new()
    {
        {"Availability(H1, 20240901, SGL)", "2"},
        {$"Availability(H1, 20240901, SGL){Environment.NewLine}Search(H1, 0, SGL)", $"2{Environment.NewLine}(20240901, 2)"},
    };

    [Theory]
    [MemberData(nameof(ValidInput))]
    public async Task ValidCommands_LogWithoutError(string input, string expected)
    {
        // Arrange
        ConsoleHelper.SetupIn(input);
        using var @out = ConsoleHelper.SetupOut();

        // Act
        await InvokationHelper.StartHostAsync(new(2024, 9, 1));

        // Assert
        @out.AssertNoErrorLog();
        @out.AssertLogs(expected);
    }
}
