using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace ConsoleApp;
static class IServiceCollectionExtensions
{
    public static IServiceCollection AddBaseDependencies(this IServiceCollection services)
        => services
            .AddArgsSupport()
            .AddCommandsSupport()
            .AddWrappers()
            .AddSingleton<QueryingAlgorithm>()
            .AddHostedService<Runner>();

    public static IServiceCollection AddCommand<TCommand, TParser, THandler>(this IServiceCollection services)
        where TCommand : ICommand
        where TParser : class, ICommandParser
        where THandler : class, ICommandHandler<TCommand>
    {
        return services
            .AddSingleton<ICommandParser, TParser>()
            .AddSingleton<ICommandHandler<TCommand>, THandler>();
    }

    private static IServiceCollection AddArgsSupport(this IServiceCollection services)
        => services
            .AddSingleton(new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new DateOnlyConverter() },
            })
            .AddSingleton<ArgFactory>()
            .AddSingleton<ArgsProvider>();

    private static IServiceCollection AddCommandsSupport(this IServiceCollection services)
        => services
            .AddSingleton<CommandParser>()
            .AddSingleton<CommandHandlerRunner>();

    private static IServiceCollection AddWrappers(this IServiceCollection services)
        => services
            .AddSingleton(TimeProvider.System)
            .AddSingleton<IFile, FileWrapper>()
            .AddSingleton<IConsole, ConsoleWrapper>();
}
