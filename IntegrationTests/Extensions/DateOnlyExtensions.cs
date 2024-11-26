namespace IntegrationTests;
static class DateOnlyExtensions
{
    public static DateTimeOffset ToDateTimeOffset(this DateOnly date)
        => new(date.ToDateTime(TimeOnly.MinValue));
}
