using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;
public abstract class TestBase
{
    protected FakeConsole Console { get; } = new();
    protected FakeFile File { get; } = new();

    protected Task RunAsync()
        => RunAsync(DateOnly.FromDateTime(DateTime.UtcNow));

    protected Task RunAsync(DateOnly startDate)
        => InvokationHelper.StartHostAsync(startDate, services => services
            .AddSingleton<IConsole, FakeConsole>(_ => Console)
            .AddSingleton<IFile, FakeFile>(_ => File));
}
