using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleApp;
class HostHelper
{
    public static HostApplicationBuilder CreateBuilder(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration.AddCommandLine(args);

        builder.Logging.ClearProviders();

        builder.Services
            .AddBaseDependencies()
            .AddCommand<AvailabilityCommand, AvailabilityCommandParser, AvailabilityCommandHandler>()
            .AddCommand<SearchCommand, SearchCommandParser, SearchHandler>();

        return builder;
    }
}
