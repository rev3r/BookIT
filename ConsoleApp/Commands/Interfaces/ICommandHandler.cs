namespace ConsoleApp;
interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    public IOutputableResult Handle(TCommand command);
}
