namespace ConsoleApp;
class QueryingAlgorithm(ArgsProvider provider)
{
    public int GetRelevantRoomCount(string hotelId, string roomType)
    {
        var hotel = provider.Hotels.FirstOrDefault(h => h.Id == hotelId);
        if (hotel is null)
            return 0;

        if (hotel.RoomTypes.Any(rt => rt.Code == roomType) is false)
            return 0;

        return hotel.Rooms.Count(r => r.RoomType == roomType);
    }

    public SortedDictionary<DateOnly, int> GetAssignmentsPerDay(
        string hotelId, string roomType, DateOnly from, DateOnly to)
    {
        var assignments = new SortedDictionary<DateOnly, int>();

        var bookings = GetRelevantBookings(hotelId, roomType, from, to);
        foreach (var booking in bookings)
        {
            var startDate = booking.Arrival > from
                ? booking.Arrival
                : from;

            var endDate = booking.Departure.AddDays(-1) < to
                ? booking.Departure.AddDays(-1)
                : to;

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var count = assignments.GetValueOrDefault(date);
                assignments[date] = count + 1;
            }
        }

        return assignments;
    }

    private IEnumerable<Booking> GetRelevantBookings(
        string hotelId, string roomType, DateOnly from, DateOnly to)
    {
        return provider.Bookings.Where(b =>
            b.HotelId == hotelId
            && b.RoomType == roomType
            && b.Departure > from
            && b.Arrival <= to);
    }
}
