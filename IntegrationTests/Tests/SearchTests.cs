namespace IntegrationTests;
public class SearchTests : TestBase
{
    private const string YearMonth = "202409";

    [Fact]
    public async Task NoRooms_ReturnEmpty()
    {
        // Arrange
        SetupFakes(
            roomsCount: 0,
            dayRanges: [],
            inputDaysAhead: 5);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput("");
    }

    #region SingleDay

    [Fact]
    public async Task SameArrivalAndDeparture_IgnoreSuchBooking()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(5, 5)],
            inputDaysAhead: 0);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05, 2)");
    }

    [Fact]
    public async Task SameArrivalAndInput_ReturnReducedRange()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(5, 10)],
            inputDaysAhead: 0);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05, 1)");
    }

    [Fact]
    public async Task SameDepartureAndInput_IgnoreSuchBooking()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 5)],
            inputDaysAhead: 0);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05, 2)");
    }

    [Fact]
    public async Task InputWithinBookingRange_ReturnReducedRange()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10)],
            inputDaysAhead: 0);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05, 1)");
    }

    [Fact]
    public async Task BookingRangeOutsideInput_IgnoreSuchBookings()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 3), (7, 10)],
            inputDaysAhead: 0);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05, 2)");
    }

    [Fact]
    public async Task AllRoomsFilled_ReturnEmpty()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10), (1, 10)],
            inputDaysAhead: 0);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput("");
    }

    [Fact]
    public async Task Overbooked_ReturnEmpty()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10), (4, 12), (5, 6), (1, 30)],
            inputDaysAhead: 0);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput("");
    }

    #endregion
    #region DateRange

    [Fact]
    public async Task InputDatesSameAsBookingRange_ReturnReducedRangeForLastDay()
    {
        // Arrange
        SetupFakes(
            roomsCount: 3,
            dayRanges: [(5, 6)],
            inputDaysAhead: 1);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05, 2), ({YearMonth}06, 3)");
    }

    [Fact]
    public async Task SameArrivalAndInputFrom_DepartureInsideInputRange_ReturnReducedRange()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(5, 6)],
            inputDaysAhead: 2);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05, 1), ({YearMonth}06-{YearMonth}07, 2)");
    }

    [Fact]
    public async Task SameArrivalAndInputFrom_DepartureOutsideInputRange_ReturnReducedRange()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(5, 10)],
            inputDaysAhead: 1);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05-{YearMonth}06, 1)");
    }

    [Fact]
    public async Task SameDepartureAndInputFrom_IgnoreSuchBooking()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(3, 5)],
            inputDaysAhead: 1);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05-{YearMonth}06, 2)");
    }

    [Fact]
    public async Task InputDatesWithinBookingRange_ReturnReducedRange()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10)],
            inputDaysAhead: 2);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05-{YearMonth}07, 1)");
    }

    [Fact]
    public async Task BookingRangeWithinInputDates_ReturnReducedRange()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(5, 6)],
            inputDaysAhead: 5);

        // Act
        await RunAsync(3);

        // Assert
        Console.AssertOutput($"({YearMonth}03-{YearMonth}04, 2), ({YearMonth}05, 1), ({YearMonth}06-{YearMonth}08, 2)");
    }

    [Fact]
    public async Task BookingsOutsideInputRange_IgnoreSuchBookings()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 3), (7, 10)],
            inputDaysAhead: 1);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05-{YearMonth}06, 2)");
    }

    [Fact]
    public async Task ArrivalWithinInputRange_ReturnReducedRange()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(6, 8)],
            inputDaysAhead: 2);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05, 2), ({YearMonth}06-{YearMonth}07, 1)");
    }

    [Fact]
    public async Task DepartureWithinInputRange_ReturnReducedRange()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 6)],
            inputDaysAhead: 2);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput($"({YearMonth}05, 1), ({YearMonth}06-{YearMonth}07, 2)");
    }

    [Fact]
    public async Task AllRoomsFilledWithinInputRange_ReturnEmpty()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10), (1, 10)],
            inputDaysAhead: 3);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput("");
    }

    [Fact]
    public async Task OverbookedWithinRange_ReturnEmpty()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dayRanges: [(1, 10), (4, 12), (5, 7), (1, 30)],
            inputDaysAhead: 1);

        // Act
        await RunAsync(5);

        // Assert
        Console.AssertOutput("");
    }

    [Fact]
    public async Task SpanningAcrossMonthsAndYears_ReturnCorrectRange()
    {
        // Arrange
        SetupFakes(
            roomsCount: 2,
            dateRanges: [(new DateOnly(2025, 1, 2), new DateOnly(2025, 1, 10))],
            inputDaysAhead: 4);

        // Act
        await RunAsync(new DateOnly(2024, 12, 30));

        // Assert
        Console.AssertOutput("(20241230-20250101, 2), (20250102-20250103, 1)");
    }

    #endregion

    private void SetupFakes(int roomsCount, (int arrival, int departure)[] dayRanges, int inputDaysAhead)
    {
        var baseDate = new DateOnly(2024, 8, 31);

        var dateRanges = dayRanges
            .Select(r => (baseDate.AddDays(r.arrival), baseDate.AddDays(r.departure)))
            .ToArray();

        SetupFakes(roomsCount, dateRanges, inputDaysAhead);
    }

    private void SetupFakes(
        int roomsCount,
        (DateOnly arrival, DateOnly departure)[] dateRanges,
        int inputDaysAhead)
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

        var input = CreateInput("H1", inputDaysAhead, SingleRoomType);
        Console.SetupInput(input);
    }

    private static string CreateInput(string hotelId, int daysAhead, string roomType)
        => $"Search({hotelId}, {daysAhead}, {roomType})";

    private Task RunAsync(int day)
        => RunAsync(new DateOnly(2024, 9, day));
}
