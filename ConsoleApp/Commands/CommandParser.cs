using FluentResults;

namespace ConsoleApp;
class CommandParser(IEnumerable<ICommandParser> parsers)
{
    public Result<ICommand> Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Result.Fail("InputIsEmpty");

        var paramsStartIndex = input.IndexOf('(');
        if (paramsStartIndex == -1)
            return Result.Fail("NoOpenRoundBracketCharacter");
        if (paramsStartIndex == 0)
            return Result.Fail("NoCommandName");

        var paramsEndIndex = input[paramsStartIndex..].IndexOf(')');
        if (paramsEndIndex == -1)
            return Result.Fail("NoClosingRoundBracketCharacter");

        var commandKind = input[..paramsStartIndex].Trim();
        var commandParams = input
            .Substring(paramsStartIndex + 1, paramsEndIndex - 1)
            .Split(',')
            .Select(@param => param.Trim())
            .ToArray();

        var parser = parsers.FirstOrDefault(f => f.HandledKind.Equals(commandKind, StringComparison.OrdinalIgnoreCase));
        if (parser is null)
            return Result.Fail("SpecificParserNotFound");

        return parser.Parse(commandParams);
    }
}
