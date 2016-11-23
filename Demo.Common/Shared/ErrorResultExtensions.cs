namespace Demo.Common.Shared
{
    using System;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public static class ErrorResultExtensions
    {
        public static IResult<Error> ToGeneric(this NonEmptyString message)
        {
            return Result<Error>.Fail(Error.CreateGeneric(message));
        }

        public static IResult<T, Error> ToGeneric<T>(this NonEmptyString message)
        {
            return Result<T, Error>.Fail(Error.CreateGeneric(message));
        }

        public static IResult<T, Error> ToNotFound<T>()
        {
            return Result<T, Error>.Fail(Error.CreateNotFound());
        }

        public static IResult<Error> ToNotFound()
        {
            return Result<Error>.Fail(Error.CreateNotFound());
        }

        public static IResult<TNextResult, Error> OnSuccess<TCurrentResult, TNextResult, TCurrentError>(this IResult<TCurrentResult, TCurrentError> result, Func<TCurrentResult, IResult<TNextResult, Error>> nextFunc, Func<TCurrentError, Error> errorConverterFunc)
            where TCurrentError : class
        {
            return ResultExtensions.OnSuccess(result, nextFunc, errorConverterFunc);
        }

        public static IResult<TNextResult, Error> OnSuccess<TCurrentResult, TNextResult>(this IResult<TCurrentResult, Error> result, Func<TCurrentResult, IResult<TNextResult, Error>> nextFunc)
        {
            return ResultExtensions.OnSuccess(result, nextFunc);
        }

        public static IResult<Error> OnSuccess<TResult, TError>(this IResult<TResult, TError> result, Func<TResult, IResult<Error>> nextFunc, Func<TError, Error> errorConverterFunc)
            where TError : class
        {
            return ResultExtensions.OnSuccess(result, nextFunc, errorConverterFunc);
        }

        public static IResult<Error> OnSuccess<TResult>(this IResult<TResult, Error> result, Func<TResult, IResult<Error>> nextFunc)
        {
            return ResultExtensions.OnSuccess(result, nextFunc);
        }
    }
}
