using FluentResults;

namespace ConsoleApp;
static class ResultExtensions
{
    public static Result<T> ToResult<T>(this T? value, string errorMessage)
        => value is null
            ? Result.Fail(errorMessage)
            : Result.Ok(value);

    public static Result PropagateError<T>(this Result<T> origin)
        => Result.Fail(origin.Errors);

    public static string GetMessage<T>(this Result<T> result)
        => result.IsFailed
            ? result.Reasons.OfType<IError>().First().Message
            : "";
}
