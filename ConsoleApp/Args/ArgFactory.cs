using FluentResults;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ConsoleApp;
class ArgFactory(
    IConfiguration config,
    IFile file,
    JsonSerializerOptions serializerOptions,
    ArgsProvider provider)
{
    public Result<TArg> Create<TArg>(string argName)
    {
        var pathToJson = config[argName];

        if (string.IsNullOrWhiteSpace(pathToJson))
            return Result.Fail("PathNotProvided");

        if (file.Exists(pathToJson) is false)
            return Result.Fail("FileNotExists");

        if (TryOpen(pathToJson, out var fileStream) is false)
            return Result.Fail("FileCantBeOpened");

        if (fileStream!.Length == 0)
            return Result.Fail("FileIsEmpty");

        if (TryDeserialize<TArg>(fileStream, out var arg) is false)
            return Result.Fail("ContentCantBeDeserialized");

        provider.Set(arg!);
        return arg!;
    }

    private bool TryOpen(string pathToJson, out FileStream? result)
    {
        try
        {
            result = file.OpenRead(pathToJson);
            return true;
        }
        catch (Exception)
        {
            result = null;
            return false;
        }
    }

    private bool TryDeserialize<TArg>(FileStream stream, out TArg? result)
    {
        try
        {
            result = JsonSerializer.Deserialize<TArg>(stream, serializerOptions);
            return result is not null;
        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }
}