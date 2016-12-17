namespace Demo.Types
{
    using Demo.Types.FunctionalExtensions;

    public sealed class PositiveInt : SimpleStructValueObject<PositiveInt, int>
    {
        private PositiveInt(int value)
            : base(value)
        {
        }

        public static explicit operator PositiveInt(int value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => Create(value, (NonEmptyString)"Value"));
        }

        public static implicit operator int(PositiveInt value)
        {
            return value.Value;
        }

        public static IResult<PositiveInt, NonEmptyString> Create(int? value, NonEmptyString field)
        {
            return CreateInt(value, field, v => Create(v, field));
        }

        public static IResult<PositiveInt, NonEmptyString> Create(int value, NonEmptyString field)
        {
            return CreateInt(value, (NonEmptyString)(field + " can't be less or equal to zero"), v => v > 0, v => new PositiveInt(v));
        }
    }
}
