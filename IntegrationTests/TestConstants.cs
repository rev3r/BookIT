using static ConsoleApp.Constants;

namespace IntegrationTests;
static class TestConstants
{
    public const string HotelsFileName = $"{HotelsArgName}.json";
    public const string BookingsFileName = $"{BookingsArgName}.json";

    public static readonly string[] Args =
    [
        $"--{HotelsArgName}", HotelsFileName,
        $"--{BookingsArgName}", BookingsFileName
    ];

    public static readonly string SingleRoomType = "SGL";
    public static readonly string DoubleRoomType = "DBL";
    public static readonly RoomType[] RoomTypes = [new(SingleRoomType), new(DoubleRoomType)];
    public static readonly Room SingleRoom = new(SingleRoomType);
    public static readonly Room DoubleRoom = new(DoubleRoomType);
}
