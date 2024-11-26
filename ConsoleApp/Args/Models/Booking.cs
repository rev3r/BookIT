namespace ConsoleApp;
public record Booking(
    string HotelId,
    DateOnly Arrival,
    DateOnly Departure,
    string RoomType);
