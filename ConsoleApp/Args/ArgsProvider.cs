namespace ConsoleApp;
class ArgsProvider
{
    private Hotel[]? _hotels;
    private Booking[]? _bookings;

    public Hotel[] Hotels => _hotels ?? throw CreateGetException();
    public Booking[] Bookings => _bookings ?? throw CreateGetException();

    public void Set<TArg>(TArg arg)
    {
        if (arg is Hotel[] hotels)
            _hotels = hotels;
        else if (arg is Booking[] bookings)
            _bookings = bookings;
        else
            throw new InvalidOperationException("Value's type not supported.");
    }

    private static InvalidOperationException CreateGetException()
        => new("Value was not set before.");
}
