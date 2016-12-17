namespace Demo.Types.FunctionalExtensions
{
    using System;

    public abstract class SimpleStructValueObject<TReturned, TFrom> : ValueObject<TReturned>
        where TReturned : SimpleStructValueObject<TReturned, TFrom>
        where TFrom : struct
    {
        protected SimpleStructValueObject(TFrom value)
        {
            Value = value;
        }

        public TFrom Value { get; }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected static IResult<TReturned, NonEmptyString> CreateInt(TFrom? value, NonEmptyString field, Func<TFrom, IResult<TReturned, NonEmptyString>> createFunc)
        {
            return value == null ? GetFailResult((NonEmptyString)"{0} can't be null", field) : createFunc(value.Value);
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