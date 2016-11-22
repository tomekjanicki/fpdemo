namespace Demo.Types.FunctionalExtensions
{
    public interface IResult<out TResult, out TError> : IResult<TError>
        where TError : class
    {
        TResult Value { get; }
    }

    public interface IResult<out TError>
        where TError : class
    {
        bool IsFailure { get; }

        bool IsSuccess { get; }

        TError Error { get; }
    }
}