namespace Demo.Types
{
    using Demo.Types.FunctionalExtensions;

    public sealed class NonEmptyLowerCaseString : SimpleClassValueObject<NonEmptyLowerCaseString, string>
    {
        private NonEmptyLowerCaseString(string value)
            : base(value)
        {
        }

        public static explicit operator NonEmptyLowerCaseString(string value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator string(NonEmptyLowerCaseString value)
        {
            return value.Value;
        }

        public static implicit operator NonEmptyString(NonEmptyLowerCaseString value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => NonEmptyString.TryCreate(value, (NonEmptyString)"Value"));
        }

        public static IResult<NonEmptyLowerCaseString, NonEmptyString> TryCreate(string value, NonEmptyString field)
        {
            return TryCreateInt(value, (NonEmptyString)(field + "can't be empty"), s => s != string.Empty, s => new NonEmptyLowerCaseString(s.ToLower()));
        }
    }
}