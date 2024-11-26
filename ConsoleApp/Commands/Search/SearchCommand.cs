namespace ConsoleApp;
record SearchCommand(
    string HotelId,
    int DaysAhead,
    string RoomType)
    : ICommand;
