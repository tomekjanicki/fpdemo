namespace Demo.Types.FunctionalExtensions
{
    using System;
    using NullGuard;

    public sealed class Result<TError> : BaseResult<bool, TError>
        where TError : class
    {
        private Result(Maybe<TError> error)
            : base(true, error, false)
        {
        }

        public static Result<TError> Ok()
        {
            return new Result<TError>(null);
        }

        public static Result<TError> Fail(TError error)
        {
            return new Result<TError>(error);
        }
    }

    public sealed class Result<TResult, TError> : BaseResult<TResult, TError>, IResult<TResult, TError>
        where TError : class
    {
        private Result([AllowNull]TResult value, Maybe<TError> error)
            : base(value, error, true)
        {
        }

        public TResult Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException("There is no value for failure.");
                }

                return Val;
            }
        }

        public static Result<TResult, TError> Ok(TResult value)
        {
            return new Result<TResult, TError>(value, null);
        }

        public static Result<TResult, TError> Fail(TError error)
        {
            return new Result<TResult, TError>(default(TResult), error);
        }
    }
}
