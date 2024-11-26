namespace ConsoleApp;
record AvailabilityCommand(
    string HotelId,
    DateOnly Arrival,
    DateOnly Departure,
    string RoomType)
    : ICommand;
