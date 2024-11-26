using FluentResults;

namespace UnitTests;
public class CommandParserTests
{
    private readonly CommandParser _parser = new([new TestCommandParser()]);

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("test")]
    [InlineData("test(")]
    [InlineData("test)")]
    public void InvalidCommand_Fail(string value)
    {
        var result = _parser.Parse(value);

        Assert.True(result.IsFailed);
    }

    [Fact]
    public void NoProperCommandHandler_Fail()
    {
        var result = _parser.Parse("foo()");

        Assert.True(result.IsFailed);
    }

    [Theory]
    [InlineData("test()")]
    [InlineData("test(a,b,c)")]
    [InlineData("  test  (  a  ,  b  ,  c  )  ")]
    public void ValidCommand_Success(string value)
    {
        var result = _parser.Parse(value);

        Assert.True(result.IsSuccess);
    }

    class TestCommandParser : ICommandParser
    {
        public string HandledKind => "test";
        public Result<ICommand> Parse(string[] @params) => Result.Ok();
    }
}
