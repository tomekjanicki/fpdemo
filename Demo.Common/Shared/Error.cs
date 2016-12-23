namespace Demo.Common.Shared
{
    using System;
    using System.Collections.Immutable;
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public sealed class Error : ValueObject<Error>
    {
        private readonly string _message;

        private Error(ErrorType errorType, string message)
        {
            ErrorType = errorType;
            _message = message;
        }

        public ErrorType ErrorType { get; }

        public string Message
        {
            get
            {
                if (ErrorType != ErrorType.Generic)
                {
                    throw new InvalidOperationException($"There is no message for others than {ErrorType.Generic}.");
                }

                return _message;
            }
        }

        public static Error CreateGeneric(NonEmptyString message)
        {
            return new Error(ErrorType.Generic, message);
        }

        public static Error CreateNotFound()
        {
            return new Error(ErrorType.NotFound, string.Empty);
        }

        protected override bool EqualsCore(Error other)
        {
            return ErrorType == other.ErrorType && _message == other._message;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { ErrorType, _message }.ToImmutableList());
        }
    }
}