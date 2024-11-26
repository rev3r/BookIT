namespace UnitTests;
public class QueryingAlgorithmTests
{
    private readonly QueryingAlgorithm _algorithm = CreateAlgorithm();

    [Fact]
    public void SuchRoomExists_ReturnCount()
    {
        var result = _algorithm.GetRelevantRoomCount("H1", "SGL");

        Assert.Equal(2, result);
    }

    [Fact]
    public void WrongHotelId_ReturnZero()
    {
        var result = _algorithm.GetRelevantRoomCount("H2", "SGL");

        Assert.Equal(0, result);
    }

    [Fact]
    public void WrongRoomType_ReturnZero()
    {
        var result = _algorithm.GetRelevantRoomCount("H1", "DBL");

        Assert.Equal(0, result);
    }

    private static QueryingAlgorithm CreateAlgorithm()
    {
        var hotel = new Hotel("H1", [new("SGL")], [new("SGL"), new("SGL")]);

        var provider = new ArgsProvider();
        provider.Set<Hotel[]>([hotel]);

        return new QueryingAlgorithm(provider);
    }
}
