using FluentResults;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace UnitTests;
public class ArgFactoryTests
{
    private const string ArgName = "test";
    private const string Path = "foo/test.json";
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        RespectRequiredConstructorParameters = true,
    };

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void InvalidPath_Fail(string? path)
    {
        // Arrange
        var file = new FakeFile().SetupFile(new EmptyArg());
        var service = CreateService(path, file);

        // Act
        var result = Run<EmptyArg>(service);

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact]
    public void FileNotExists_Fail()
    {
        // Arrange
        var file = new FakeFile().FileNotExists();
        var service = CreateService(Path, file);

        // Act
        var result = Run<EmptyArg>(service);

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact]
    public void ThrowingWhenOpening_Fail()
    {
        // Arrange
        var file = new FakeFile().OpenThrows();
        var service = CreateService(Path, file);

        // Act
        var result = Run<EmptyArg>(service);

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact]
    public void EmptyFile_Fail()
    {
        // Arrange
        var file = new FakeFile();
        var service = CreateService(Path, file);

        // Act
        var result = Run<EmptyArg>(service);

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact]
    public void InvalidShape_Fail()
    {
        // Arrange
        var file = new FakeFile().SetupFile(new EmptyArg());
        var service = CreateService(Path, file);

        // Act
        var result = Run<ComplexArg>(service);

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact]
    public void Valid_Success()
    {
        // Arrange
        var hotel = new Hotel("H1", [], []);
        var file = new FakeFile().SetupFile<Hotel[]>([hotel]);
        var service = CreateService(Path, file);

        // Act
        var result = Run<Hotel[]>(service);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("H1", result.Value[0].Id);
    }

    private static ArgFactory CreateService(string? path, IFile file)
        => new(SetupConfiguration(path), file, SerializerOptions, new());

    private static IConfiguration SetupConfiguration(string? path)
        => new ConfigurationBuilder()
            .AddInMemoryCollection([KeyValuePair.Create(ArgName, path)!])
            .Build();

    private static Result<TArg> Run<TArg>(ArgFactory service)
        => service.Create<TArg>(ArgName);

    record EmptyArg();
    record ComplexArg(int Number);

    class FakeFile : IFile
    {
        private readonly string _realPath = Guid.NewGuid() + ".json";
        private string? _file = null;
        private bool _exists = true;
        private bool _shouldReadThrow = false;

        public FakeFile SetupFile<TArg>(TArg arg)
        {
            _file = JsonSerializer.Serialize(arg);
            return this;
        }

        public FakeFile FileNotExists()
        {
            _exists = false;
            return this;
        }

        public FakeFile OpenThrows()
        {
            _shouldReadThrow = true;
            return this;
        }

        public bool Exists(string path)
            => _exists;

        public FileStream OpenRead(string path)
        {
            if (_shouldReadThrow)
                throw new Exception();

            var content = _file is not null
                ? Encoding.UTF8.GetBytes(_file)
                : [];

            var stream = File.Create(_realPath);
            stream.Write(content);
            stream.Position = 0;

            return stream;
        }

        public void Dispose()
        {
            if (_file is not null)
                File.Delete(_realPath);
        }
    }
}
