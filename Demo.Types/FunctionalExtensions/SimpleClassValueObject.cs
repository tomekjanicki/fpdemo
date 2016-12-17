namespace Demo.Types.FunctionalExtensions
{
    using System;

    public abstract class SimpleClassValueObject<TReturned, TFrom> : ValueObject<TReturned>
        where TReturned : SimpleClassValueObject<TReturned, TFrom>
        where TFrom : class
    {
        protected SimpleClassValueObject(TFrom value)
        {
            Value = value;
        }

        public TFrom Value { get; }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected static IResult<TReturned, NonEmptyString> CreateInt(TFrom value, NonEmptyString errorMessage, Func<TFrom, bool> isValidFunc, Func<TFrom, TReturned> newInstanceFunc)
        {
            return !isValidFunc(value) ? GetFailResult(errorMessage) : GetOkResult(newInstanceFunc(value));
        }

        protected override bool EqualsCore(TReturned other)
        {
            return Value.Equals(other.Value);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}