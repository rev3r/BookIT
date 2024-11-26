namespace ConsoleApp;
class AvailabilityCommandHandler(QueryingAlgorithm algorithm)
    : ICommandHandler<AvailabilityCommand>
{
    public IOutputableResult Handle(AvailabilityCommand command)
    {
        var roomCount = algorithm.GetRelevantRoomCount(command.HotelId, command.RoomType);
        if (roomCount == 0)
            return AvailabilityResult.Empty;

        var from = command.Arrival;
        var to = command.Departure;

        var assignmentsPerDay = algorithm.GetAssignmentsPerDay(
            command.HotelId, command.RoomType, from, to);

        var assignmentsCount = 0;
        for (var date = from; date <= to; date = date.AddDays(1))
        {
            assignmentsCount += assignmentsPerDay.GetValueOrDefault(date);
        }

        var daysCount = to.DayNumber - from.DayNumber + 1;
        return new AvailabilityResult((daysCount * roomCount) - assignmentsCount);
    }
}