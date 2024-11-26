using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp;
class CommandHandlerRunner(IServiceProvider serviceProvider)
{
    private readonly Dictionary<Type, Type> _commandToHandlerMapping = [];

    public IOutputableResult Run<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        var commandType = command.GetType();
        var handlerType = _commandToHandlerMapping.TryGetValue(commandType, out var value)
            ? value
            : typeof(ICommandHandler<>).MakeGenericType(commandType);

        var handler = serviceProvider.GetRequiredService(handlerType);

        var result = handlerType
            .GetMethod(nameof(ICommandHandler<ICommand>.Handle))!
            .Invoke(handler, [command]);

        return (IOutputableResult)result!;
    }
}
