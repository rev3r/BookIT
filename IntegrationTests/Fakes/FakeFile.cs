using System.Text;
using System.Text.Json;

namespace IntegrationTests;
public sealed class FakeFile : IFile, IDisposable
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new DateOnlyConverter() },
        WriteIndented = true,
    };

    private readonly Dictionary<string, string> _files = [];
    private readonly List<string> _realPaths = [];

    public void SetupFiles(Hotel[] hotels, Booking[] bookings)
    {
        SetupFile(HotelsFileName, hotels);
        SetupFile(BookingsFileName, bookings);
    }

    public FileStream OpenRead(string path)
        => GenerateFile(_files[path]);

    public bool Exists(string path)
        => _files.ContainsKey(path);

    private void SetupFile<TFile>(string path, TFile content)
        => _files[path] = JsonSerializer.Serialize(content, SerializerOptions);

    private FileStream GenerateFile(string content)
    {
        var realPath = Guid.NewGuid() + ".json";
        _realPaths.Add(realPath);

        var stream = File.Create(realPath);
        stream.Write(Encoding.UTF8.GetBytes(content));
        stream.Position = 0;

        return stream;
    }

    public void Dispose()
    {
        foreach (var path in _realPaths)
            File.Delete(path);
    }
}
