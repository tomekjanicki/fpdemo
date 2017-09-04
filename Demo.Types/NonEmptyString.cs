namespace Demo.Types
{
    using Demo.Types.FunctionalExtensions;

    public sealed class NonEmptyString : SimpleClassValueObject<NonEmptyString, string>
    {
        private NonEmptyString(string value)
            : base(value)
        {
        }

        public static explicit operator NonEmptyString(string value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => TryCreate(value, new NonEmptyString("Value")));
        }

        public static implicit operator string(NonEmptyString value)
        {
            return value.Value;
        }

        public static IResult<NonEmptyString, NonEmptyString> TryCreate(string value, NonEmptyString field)
        {
            return TryCreateInt(value, new NonEmptyString($"{field.Value} can't be empty"), s => s != string.Empty, s => new NonEmptyString(s));
        }
    }
}
