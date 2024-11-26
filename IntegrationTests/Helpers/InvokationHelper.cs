using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using System.Reflection;

namespace IntegrationTests;
static class InvokationHelper
{
    private static readonly MethodInfo _entryPoint = typeof(Program).Assembly.EntryPoint!;

    public static void RunProgram(string[] args)
        => _entryPoint.Invoke(null, [args]);

    public static Task StartHostAsync(
        DateOnly startDate, Action<IServiceCollection>? addServices = null)
    {
        var builder = HostHelper.CreateBuilder(TestConstants.Args);

        var start = startDate.ToDateTimeOffset();
        builder.Services.AddSingleton<TimeProvider>(new FakeTimeProvider(start));

        addServices?.Invoke(builder.Services);

        return builder.Build().StartAsync();
    }
}
