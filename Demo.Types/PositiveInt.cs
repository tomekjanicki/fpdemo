namespace Demo.Types
{
    using Demo.Types.FunctionalExtensions;

    public sealed class PositiveInt : ValueObject<PositiveInt>
    {
        private PositiveInt(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static explicit operator PositiveInt(int value)
        {
            return GetValue(() => Create(value, (NonEmptyString)"Value"));
        }

        public static implicit operator int(PositiveInt value)
        {
            return value.Value;
        }

        public static IResult<PositiveInt, NonEmptyString> Create(int? value, NonEmptyString field)
        {
            return value == null ? GetFailResult((NonEmptyString)"{0} can't be null", field) : Create(value.Value, field);
        }

        public static IResult<PositiveInt, NonEmptyString> Create(int value, NonEmptyString field)
        {
            return value <= 0 ? GetFailResult((NonEmptyString)"{0} can't be less or equal to zero", field) : GetOkResult(new PositiveInt(value));
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override bool EqualsCore(PositiveInt other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
