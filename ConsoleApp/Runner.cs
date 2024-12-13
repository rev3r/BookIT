using Microsoft.Extensions.Hosting;

namespace ConsoleApp;
class Runner(
    ArgFactory argFactory,
    IConsole console,
    CommandParser parser,
    CommandHandlerRunner handlerRunner)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken token)
    {
        try
        {
            if (TryCreateArgs(out _) is false)
                return Task.CompletedTask;

            while (token.IsCancellationRequested is false)
            {
                var line = console.ReadLine();

                if (ShouldTerminate(line))
                {
                    console.SetTopPosition(-1);
                    break;
                }

                ExecuteCommand(line!);
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }

        return Task.CompletedTask;
    }

    private bool TryCreateArgs(out Args? args)
    {
        args = null;

        if (TryCreateArg<Hotel[]>(HotelsArgName, out var hotels) is false)
            return false;

        if (TryCreateArg<Booking[]>(BookingsArgName, out var bookings) is false)
            return false;

        args = new(hotels!, bookings!);
        return true;
    }

    private bool TryCreateArg<TArg>(string argName, out TArg? arg)
    {
        var argResult = argFactory.Create<TArg>(argName);
        arg = argResult.ValueOrDefault;

        if (argResult.IsFailed)
            console.WriteErrorLine($"Invalid {argName} arg ({argResult.GetMessage()}).");

        return argResult.IsSuccess;
    }

    private static bool ShouldTerminate(string? line)
        => string.IsNullOrEmpty(line) || line == Environment.NewLine;

    private void ExecuteCommand(string line)
    {
        var commandResult = parser.Parse(line);
        if (commandResult.IsFailed)
        {
            console.WriteErrorLine($"Invalid command ({commandResult.GetMessage()}).");
            return;
        }

        var result = handlerRunner.Run(commandResult.Value);
        console.WriteInfoLine(result.ToOutputString());
    }

    private void HandleException(Exception ex)
        => console.WriteErrorLine($"Unhandled exception was thrown ({ex.Message}).");
}
