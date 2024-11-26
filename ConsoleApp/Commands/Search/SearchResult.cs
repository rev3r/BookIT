namespace ConsoleApp;

record SearchResult(IEnumerable<SearchResultRow> Rows) : IOutputableResult
{
    public static SearchResult Empty { get; } = new([]);

    public string ToOutputString()
    {
        var outputRows = Rows.Select(row => row.ToOutputString());
        return string.Join(", ", outputRows);
    }
}

record SearchResultRow(DateOnly From, DateOnly To, int AvailabilityCount) : IOutputableResult
{
    public DateOnly To { get; set; } = To;

    public string ToOutputString()
        => From == To
            ? $"({From.ToFormattedString()}, {AvailabilityCount})"
            : $"({From.ToFormattedString()}-{To.ToFormattedString()}, {AvailabilityCount})";
}