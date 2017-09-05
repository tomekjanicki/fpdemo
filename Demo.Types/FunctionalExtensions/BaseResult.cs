namespace Demo.Types.FunctionalExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using NullGuard;

    public abstract class BaseResult<TResult, TError> : ValueObject<BaseResult<TResult, TError>>, IResult<TError>
        where TError : class
    {
        private readonly Maybe<TError> _error;
        private readonly bool _useValueInEqualityCalculation;

        protected BaseResult([AllowNull]TResult value, Maybe<TError> error, bool useValueInEqualityCalculation)
        {
            Val = value;
            _error = error;
            _useValueInEqualityCalculation = useValueInEqualityCalculation;
            IsFailure = error.HasValue;
        }

        public bool IsFailure { get; }

        public bool IsSuccess => !IsFailure;

        public TError Error
        {
            get
            {
                if (IsSuccess)
                {
                    throw new InvalidOperationException("There is no error for success.");
                }

                return _error.Value;
            }
        }

        protected TResult Val { get; }

        protected override bool EqualsCore(BaseResult<TResult, TError> other)
        {
            if (IsFailure)
            {
                return IsSuccess == other.IsSuccess && IsFailure == other.IsFailure && Error == other.Error;
            }

            if (_useValueInEqualityCalculation)
            {
                return IsSuccess == other.IsSuccess && IsFailure == other.IsFailure && Val.Equals(other.Val);
            }

            return IsSuccess == other.IsSuccess && IsFailure == other.IsFailure;
        }

        protected override int GetHashCodeCore()
        {
            var result = new List<object> { IsSuccess, IsFailure };

            if (IsSuccess)
            {
                if (_useValueInEqualityCalculation)
                {
                    result.Add(Val);
                }
            }
            else
            {
                result.Add(Error);
            }

            return GetCalculatedHashCode(result.ToImmutableList());
        }
    }
}
