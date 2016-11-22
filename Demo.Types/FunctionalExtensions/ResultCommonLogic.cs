namespace Demo.Types.FunctionalExtensions
{
    using System;

    internal sealed class ResultCommonLogic<TError>
        where TError : class
    {
        private readonly Maybe<TError> _error;

        public ResultCommonLogic(Maybe<TError> error)
        {
            _error = error;
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
    }
}
