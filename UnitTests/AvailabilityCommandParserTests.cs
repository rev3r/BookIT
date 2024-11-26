namespace UnitTests;
public class AvailabilityCommandParserTests
{
    private readonly AvailabilityCommandParser _parser = new();

    public static readonly TheoryData<string[]> InvalidCommands =
    [
        [],
        ["H1"],
        ["H1", "20240901"],
        ["H1", "20240901", "SGL", "foo"],
        ["", "20240901", "SGL"],
        ["H1", "", "SGL"],
        ["H1", "01.09.2024", "SGL"],
        ["H1", "20240901-01.09.2024", "SGL"],
        ["H1", "20240901-", "SGL"],
        ["H1", "-20240901", "SGL"],
        ["H1", "20240901", ""],
    ];

    [Theory]
    [MemberData(nameof(InvalidCommands))]
    public void InvalidCommand_Fail(string[] @params)
    {
        var result = _parser.Parse(@params);

        Assert.True(result.IsFailed);
    }

    [Fact]
    public void ValidCommand_SingleDate_Success()
    {
        var result = _parser.Parse(["H1", "20240901", "SGL"]);

        Assert.True(result.IsSuccess);

        var command = (AvailabilityCommand)result.Value;
        Assert.Equal("H1", command.HotelId);
        Assert.Equal(new DateOnly(2024, 9, 1), command.Arrival);
        Assert.Equal(new DateOnly(2024, 9, 1), command.Departure);
        Assert.Equal("SGL", command.RoomType);
    }

    [Fact]
    public void ValidCommand_DateRange_Success()
    {
        var result = _parser.Parse(["H1", "20240901-20240902", "SGL"]);

        Assert.True(result.IsSuccess);

        var command = (AvailabilityCommand)result.Value;
        Assert.Equal("H1", command.HotelId);
        Assert.Equal(new DateOnly(2024, 9, 1), command.Arrival);
        Assert.Equal(new DateOnly(2024, 9, 2), command.Departure);
        Assert.Equal("SGL", command.RoomType);
    }
}
