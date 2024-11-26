using FluentResults;

namespace ConsoleApp;
interface ICommandParser
{
    public string HandledKind { get; }
    public Result<ICommand> Parse(string[] @params);
}
