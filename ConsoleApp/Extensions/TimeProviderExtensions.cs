namespace ConsoleApp;
static class TimeProviderExtensions
{
    public static DateOnly GetLocalToday(this TimeProvider timeProvider)
        => DateOnly.FromDateTime(timeProvider.GetLocalNow().LocalDateTime);
}
