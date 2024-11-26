namespace ConsoleApp;
class SearchHandler(TimeProvider timeProvider, QueryingAlgorithm algorithm)
    : ICommandHandler<SearchCommand>
{
    public IOutputableResult Handle(SearchCommand command)
    {
        var roomCount = algorithm.GetRelevantRoomCount(command.HotelId, command.RoomType);
        if (roomCount == 0)
            return SearchResult.Empty;

        var from = timeProvider.GetLocalToday();
        var to = from.AddDays(command.DaysAhead);

        var assignmentsPerDay = algorithm.GetAssignmentsPerDay(
            command.HotelId, command.RoomType, from, to);

        var rows = new List<SearchResultRow>(assignmentsPerDay.Count);
        SearchResultRow? current = null;
        for (var date = from; date <= to; date = date.AddDays(1))
        {
            var count = roomCount - assignmentsPerDay.GetValueOrDefault(date);

            // Break range
            if (count <= 0)
            {
                FlushRow();
                current = null;
            }
            // Start range
            else if (current is null)
            {
                current = new(date, date, count);
            }
            // Continue range
            else if (current.AvailabilityCount == count)
            {
                current.To = date;
            }
            // Finish and start new range
            else
            {
                FlushRow();
                current = new(date, date, count);
            }
        }
        FlushRow();

        return new SearchResult(rows);

        void FlushRow()
        {
            if (current is null)
                return;

            rows.Add(current);
        }
    }
}
