namespace ConsoleApp;

public record Hotel(
    string Id,
    IReadOnlyCollection<RoomType> RoomTypes,
    IReadOnlyCollection<Room> Rooms);

public record RoomType(string Code);

public record Room(string RoomType);
