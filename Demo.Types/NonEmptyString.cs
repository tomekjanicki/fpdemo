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
            return GetValueWhenSuccessOrThrowInvalidCastException(() => Create(value, new NonEmptyString("Value")));
        }

        public static implicit operator string(NonEmptyString value)
        {
            return value.Value;
        }

        public static IResult<NonEmptyString, NonEmptyString> Create(string value, NonEmptyString field)
        {
            return CreateInt(value, new NonEmptyString($"{field.Value} can't be empty"), s => s != string.Empty, s => new NonEmptyString(s));
        }
    }
}
