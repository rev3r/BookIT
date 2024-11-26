using FluentResults;

namespace ConsoleApp;
class AvailabilityCommandParser : ICommandParser
{
    public string HandledKind { get; } = "Availability";

    public Result<ICommand> Parse(string[] @params)
    {
        if (@params.Length != 3)
            return Result.Fail("IncorrectNumberOfParams");

        var hotelId = @params[0];
        if (string.IsNullOrEmpty(hotelId))
            return Result.Fail("EmptyHotelId");

        var dates = @params[1].Split('-');

        if (DateOnly.TryParseExact(dates[0], DateFormat, out var arrival) is false)
            return Result.Fail("FirstDateCantBeParsed");

        var departureResult = GetDeparture(dates, arrival);
        if (departureResult.IsFailed)
            return departureResult.PropagateError();

        var roomType = @params[2];
        if (string.IsNullOrEmpty(roomType))
            return Result.Fail("EmptyRoomType");

        return new AvailabilityCommand(hotelId, arrival, departureResult.Value, roomType);
    }

    private static Result<DateOnly> GetDeparture(string[] dates, DateOnly arrival)
    {
        if (dates.Length == 1)
        {
            return arrival;
        }
        else
        {
            return DateOnly.TryParseExact(dates[1], DateFormat, out var departure)
                ? departure
                : Result.Fail("SecondDateCantBeParsed");
        }
    }
}