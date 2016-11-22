namespace Demo.Types.FunctionalExtensions
{
    using System;
    using NullGuard;

    public struct Result<TError> : IResult<TError>
        where TError : class
    {
        private readonly ResultCommonLogic<TError> _logic;

        private Result(Maybe<TError> error)
        {
            _logic = new ResultCommonLogic<TError>(error);
        }

        public bool IsFailure => _logic.IsFailure;

        public bool IsSuccess => _logic.IsSuccess;

        public TError Error => _logic.Error;

        public static Result<TError> Ok()
        {
            return new Result<TError>(null);
        }

        public static Result<TError> Fail(TError error)
        {
            return new Result<TError>(error);
        }
    }

    public struct Result<TResult, TError> : IResult<TResult, TError>
        where TError : class
    {
        private readonly ResultCommonLogic<TError> _logic;
        private readonly TResult _value;

        private Result([AllowNull]TResult value, Maybe<TError> error)
        {
            _logic = new ResultCommonLogic<TError>(error);
            _value = value;
        }

        public bool IsFailure => _logic.IsFailure;

        public bool IsSuccess => _logic.IsSuccess;

        public TError Error => _logic.Error;

        public TResult Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException("There is no value for failure.");
                }

                return _value;
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
