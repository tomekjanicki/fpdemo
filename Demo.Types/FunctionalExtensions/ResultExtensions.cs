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

        public static IResult<TNextResult, TNextError> OnSuccess<TCurrentResult, TNextResult, TCurrentError, TNextError>(this IResult<TCurrentResult, TCurrentError> result, Func<TCurrentResult, IResult<TNextResult, TNextError>> nextFunc, Func<TCurrentError, TNextError> errorConverterFunc)
            where TCurrentError : class
            where TNextError : class
        {
            return result.IsFailure ? Result<TNextResult, TNextError>.Fail(errorConverterFunc(result.Error)) : nextFunc(result.Value);
        }

        public static IResult<TNextResult, TNextError> OnSuccess<TResult, TNextResult, TNextError>(this IResult<TResult, TNextError> result, Func<TResult, IResult<TNextResult, TNextError>> nextFunc)
            where TNextError : class
        {
            return result.IsFailure ? Result<TNextResult, TNextError>.Fail(result.Error) : nextFunc(result.Value);
        }

        public static IResult<TResult, TNextError> OnSuccess<TResult, TCurrentError, TNextError>(this IResult<TResult, TCurrentError> result, Func<TResult, IResult<TResult, TNextError>> nextFunc, Func<TCurrentError, TNextError> errorConverterFunc)
            where TCurrentError : class
            where TNextError : class
        {
            return result.IsFailure ? Result<TResult, TNextError>.Fail(errorConverterFunc(result.Error)) : nextFunc(result.Value);
        }

        public static IResult<TResult, TError> OnSuccess<TResult, TError>(this IResult<TResult, TError> result, Func<TResult, IResult<TResult, TError>> nextFunc)
            where TError : class
        {
            return result.IsFailure ? Result<TResult, TError>.Fail(result.Error) : nextFunc(result.Value);
        }

        public static IResult<TNextError> OnSuccess<TCurrentError, TNextError>(this IResult<TCurrentError> result, Func<IResult<TNextError>> nextFunc, Func<TCurrentError, TNextError> errorConverterFunc)
            where TCurrentError : class
            where TNextError : class
        {
            return result.IsFailure ? Result<TNextError>.Fail(errorConverterFunc(result.Error)) : nextFunc();
        }

        public static IResult<TError> OnSuccess<TError>(this IResult<TError> result, Func<IResult<TError>> nextFunc)
            where TError : class
        {
            return result.IsFailure ? Result<TError>.Fail(result.Error) : nextFunc();
        }

        public static IResult<TNextError> OnSuccess<TResult, TCurrentError, TNextError>(this IResult<TResult, TCurrentError> result, Func<TResult, IResult<TNextError>> nextFunc, Func<TCurrentError, TNextError> errorConverterFunc)
            where TCurrentError : class
            where TNextError : class
        {
            return result.IsFailure ? Result<TNextError>.Fail(errorConverterFunc(result.Error)) : nextFunc(result.Value);
        }

        public static IResult<TError> OnSuccess<TResult, TError>(this IResult<TResult, TError> result, Func<TResult, IResult<TError>> nextFunc)
            where TError : class
        {
            return result.IsFailure ? Result<TError>.Fail(result.Error) : nextFunc(result.Value);
        }

        public static IResult<TResult, TNextError> OnSuccess<TResult, TCurrentError, TNextError>(this IResult<TCurrentError> result, Func<IResult<TResult, TNextError>> nextFunc, Func<TCurrentError, TNextError> errorConverterFunc)
            where TCurrentError : class
            where TNextError : class
        {
            return result.IsFailure ? Result<TResult, TNextError>.Fail(errorConverterFunc(result.Error)) : nextFunc();
        }

        public static IResult<TResult, TError> OnSuccess<TResult, TError>(this IResult<TError> result, Func<IResult<TResult, TError>> nextFunc)
            where TError : class
        {
            return result.IsFailure ? Result<TResult, TError>.Fail(result.Error) : nextFunc();
        }
    }
}
