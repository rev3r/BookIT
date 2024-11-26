namespace UnitTests;
public class SearchCommandParserTests
{
    private readonly SearchCommandParser _parser = new();

    public static readonly TheoryData<string[]> InvalidCommands =
    [
        [],
        ["H1"],
        ["H1", "5"],
        ["H1", "5", "SGL", "foo"],
        ["", "5", "SGL"],
        ["H1", "", "SGL"],
        ["H1", "abc", "SGL"],
        ["H1", "5", ""],
    ];

    [Theory]
    [MemberData(nameof(InvalidCommands))]
    public void InvalidCommand_Fail(string[] @params)
    {
        var result = _parser.Parse(@params);

        Assert.True(result.IsFailed);
    }

    [Fact]
    public void ValidCommand_Success()
    {
        var result = _parser.Parse(["H1", "5", "SGL"]);

        Assert.True(result.IsSuccess);

        var command = (SearchCommand)result.Value;
        Assert.Equal("H1", command.HotelId);
        Assert.Equal(5, command.DaysAhead);
        Assert.Equal("SGL", command.RoomType);
    }
}
