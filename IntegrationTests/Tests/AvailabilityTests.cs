namespace IntegrationTests;
public class AvailabilityTests : TestBase
{
    [Fact]
    public async Task NoRooms_ReturnZero()
    {
        // Arrange
        SetupFakes(
            roomsCount: 0,
            dayRanges: [],
            inputRange: (5, 10));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput("0");
    }

    #region SingleDay

    [Fact]
    public async Task SameArrivalAndDeparture_IgnoreSuchBooking()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(5, 5)],
            inputDay: 5);

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput("2");
    }

    [Fact]
    public async Task SameArrivalAndInput_ReturnReducedCount()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(5, 10)],
            inputDay: 5);

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput("1");
    }

    [Fact]
    public async Task SameDepartureAndInput_IgnoreSuchBooking()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 5)],
            inputDay: 5);

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput("2");
    }

    [Fact]
    public async Task InputWithinBookingRange_ReturnReducedCount()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10)],
            inputDay: 5);

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput("1");
    }

    [Fact]
    public async Task BookingRangeOutsideInput_IgnoreSuchBookings()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 3), (7, 10)],
            inputDay: 5);

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput("2");
    }

    [Fact]
    public async Task AllRoomsFilled_ReturnZero()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10), (1, 10)],
            inputDay: 5);

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput("0");
    }

    [Fact]
    public async Task Overbooked_ReturnNegativeCount()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10), (4, 12), (5, 6), (1, 30)],
            inputDay: 5);

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput("-2");
    }

    #endregion
    #region Range

    [Fact]
    public async Task InputDatesSameAsBookingRange_ReturnReducedCountForLastDay()
    {
        // Arrange
        SetupFakes(
            roomsCount: 3,
            dayRanges: [(5, 6)],
            inputRange: (5, 6));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput($"{2 + 3}");
    }

    [Fact]
    public async Task SameArrivalAndInputFrom_DepartureInsideInputRange_ReturnReducedCount()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(5, 6)],
            inputRange: (5, 7));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput($"{1 + 2 + 2}");
    }

    [Fact]
    public async Task SameArrivalAndInputFrom_DepartureOutsideInputRange_ReturnReducedCount()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(5, 10)],
            inputRange: (5, 6));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput($"{1 + 1}");
    }

    [Fact]
    public async Task SameDepartureAndInputFrom_IgnoreSuchBooking()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(3, 5)],
            inputRange: (5, 6));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput($"{2 + 2}");
    }

    [Fact]
    public async Task InputDatesWithinBookingRange_ReturnReducedCount()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10)],
            inputRange: (5, 7));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput($"{1 + 1 + 1}");
    }

    [Fact]
    public async Task BookingRangeWithinInputDates_ReturnReducedCount()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(5, 6)],
            inputRange: (3, 8));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput($"{2 + 2 + 1 + 2 + 2 + 2}");
    }

    [Fact]
    public async Task BookingsOutsideInputRange_IgnoreSuchBookings()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 3), (7, 10)],
            inputRange: (5, 6));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput($"{2 + 2}");
    }

    [Fact]
    public async Task ArrivalWithinInputRange_ReturnReducedCount()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(6, 8)],
            inputRange: (5, 7));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput($"{2 + 1 + 1}");
    }

    [Fact]
    public async Task DepartureWithinInputRange_ReturnReducedCount()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 6)],
            inputRange: (5, 7));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput($"{1 + 2 + 2}");
    }

    [Fact]
    public async Task AllRoomsFilledWithinInputRange_ReturnZero()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10), (1, 10)],
            inputRange: (5, 8));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput("0");
    }

    [Fact]
    public async Task OverbookedWithinRange_ReturnNegativeCount()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10), (4, 12), (5, 7), (1, 30)],
            inputRange: (5, 6));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput($"{-2 + -2}");
    }

    [Fact]
    public async Task SpanningAcrossMonthsAndYears_ReturnCorrectCount()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dateRanges: [(new DateOnly(2025, 1, 2), new DateOnly(2025, 1, 10))],
            inputRange: (new DateOnly(2024, 12, 30), new DateOnly(2025, 1, 3)));

        // Act
        await RunAsync();

        // Assert
        Console.AssertOutput($"{2 + 2 + 2 + 1 + 1}");
    }

    #endregion

    private void SetupFakes(int roomsCount, (int arrival, int departure)[] dayRanges, int inputDay)
        => SetupFakes(roomsCount, dayRanges, (inputDay, inputDay));

    private void SetupFakes(int roomsCount, (int arrival, int departure)[] dayRanges, (int from, int to) inputRange)
    {
        var baseDate = new DateOnly(2024, 8, 31);

        var dateRanges = dayRanges
            .Select(r => (baseDate.AddDays(r.arrival), baseDate.AddDays(r.departure)))
            .ToArray();

        var inputDateRange = (baseDate.AddDays(inputRange.from), baseDate.AddDays(inputRange.to));

        SetupFakes(roomsCount, dateRanges, inputDateRange);
    }

    private void SetupFakes(
        int roomsCount,
        (DateOnly arrival, DateOnly departure)[] dateRanges,
        (DateOnly from, DateOnly to) inputRange)
    {
        var rooms = Enumerable.Range(1, roomsCount)
            .Select(_ => new Room(SingleRoomType))
            .Append(new(DoubleRoomType))
            .ToArray();

        var hotels = new[]
        {
            new Hotel("H1", RoomTypes, rooms),
            new Hotel("H2", RoomTypes, [SingleRoom]),
        };

        var bookings = dateRanges
            .Select(range => new Booking("H1", range.arrival, range.departure, SingleRoomType))
            .Append(new("H2", new(2024, 9, 1), new(2024, 9, 30), SingleRoomType))
            .ToArray();

        File.SetupFiles(hotels, bookings);

        var input = CreateInput("H1", inputRange.from, inputRange.to, SingleRoomType);
        Console.SetupInput(input);
    }

    private static string CreateInput(string hotelId, DateOnly from, DateOnly to, string roomType)
        => from == to
            ? $"Availability({hotelId}, {from.ToString(Constants.DateFormat)}, {roomType})"
            : $"Availability({hotelId}, {from.ToString(Constants.DateFormat)}-{to.ToString(Constants.DateFormat)}, {roomType})";
}
