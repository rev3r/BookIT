namespace UnitTests;
public class ArgsProviderTests
{
    private readonly ArgsProvider _service = new();

    [Fact]
    public void PropertyNotSet_Throw()
    {
        Assert.Throws<InvalidOperationException>(() => _service.Hotels);
        Assert.Throws<InvalidOperationException>(() => _service.Bookings);
    }

    [Fact]
    public void PropertySet_ReturnValue()
    {
        _service.Set(new[] { new Hotel(default!, default!, default!) });
        _service.Set(new[] { new Booking(default!, default, default, default!) });

        Assert.NotEmpty(_service.Hotels);
        Assert.NotEmpty(_service.Bookings);
    }

    [Fact]
    public void InvalidSet_Throw()
    {
        Assert.Throws<InvalidOperationException>(() =>
            _service.Set(new[] { new object() }));
    }
}
