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

        var maxAssignments = 0;
        for (var date = from; date <= to; date = date.AddDays(1))
        {
            var assignments = assignmentsPerDay.GetValueOrDefault(date);
            maxAssignments = Math.Max(maxAssignments, assignments);
        }

        return new AvailabilityResult(roomCount - maxAssignments);
    }
}