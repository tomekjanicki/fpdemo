namespace Demo.Types.FunctionalExtensions
{
    using System;
    using System.Linq;

    public static class ResultExtensions
    {
        public static void EnsureIsNotFaliure<TResult>(this IResult<TResult, NonEmptyString> result)
        {
            EnsureIsNotFaliure((IResult<NonEmptyString>)result);
        }

        public static void EnsureIsNotFaliure<TResult, TError>(this IResult<TResult, TError> result, Func<string> messageFunc)
            where TError : class
        {
            EnsureIsNotFaliure((IResult<TError>)result, messageFunc);
        }

        public static void EnsureIsNotFaliure(this IResult<NonEmptyString> result)
        {
            EnsureIsNotFaliure(result, () => result.Error.Value);
        }

        public static void EnsureIsNotFaliure<TError>(this IResult<TError> result, Func<string> messageFunc)
            where TError : class
        {
            if (result.IsFailure)
            {
                throw new InvalidOperationException(messageFunc());
            }
        }

        public static IResult<NonEmptyString> IfAtLeastOneFailCombineElseReturnOk(this IResult<NonEmptyString>[] results)
        {
            var failedResults = results.Where(result => result.IsFailure).ToList();

            if (!failedResults.Any())
            {
                return GetOkMessage();
            }

            var errorMessage = (NonEmptyString)string.Join("; ", failedResults.Select(result => result.Error.Value).ToArray());

            return errorMessage.GetFailResult();
        }

        public static IResult<T, NonEmptyString> GetFailResult<T>(this NonEmptyString message)
        {
            return Result<T, NonEmptyString>.Fail(message);
        }

        public static IResult<NonEmptyString> GetFailResult(this NonEmptyString message)
        {
            return Result<NonEmptyString>.Fail(message);
        }

        public static IResult<NonEmptyString> GetOkMessage()
        {
            return Result<NonEmptyString>.Ok();
        }

        public static IResult<T, NonEmptyString> GetOkMessage<T>(this T value)
        {
            return Result<T, NonEmptyString>.Ok(value);
        }
    }
}
