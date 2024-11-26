using FluentResults;

namespace ConsoleApp;
class SearchCommandParser : ICommandParser
{
    public string HandledKind { get; } = "Search";

    public Result<ICommand> Parse(string[] @params)
    {
        if (@params.Length != 3)
            return Result.Fail("IncorrectNumberOfParams");

        var hotelId = @params[0];
        if (string.IsNullOrEmpty(hotelId))
            return Result.Fail("EmptyHotelId");

        if (int.TryParse(@params[1], out var daysAhead) is false)
            return Result.Fail("DaysAheadCantBeParsed");

        var roomType = @params[2];
        if (string.IsNullOrEmpty(roomType))
            return Result.Fail("EmptyRoomType");

        return new SearchCommand(hotelId, daysAhead, roomType);
    }
}
