namespace Demo.Logic.CQ.ValueObjects
{
    using Demo.Types;
    using Demo.Types.FunctionalExtensions;

    public sealed class Name : SimpleClassValueObject<Name, string>
    {
        private Name(string value)
            : base(value)
        {
        }

        public static explicit operator Name(string value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator string(Name name)
        {
            return name.Value;
        }

        public static implicit operator NonEmptyString(Name value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => NonEmptyString.TryCreate(value, (NonEmptyString)"Value"));
        }

        public static IResult<Name, NonEmptyString> TryCreate(string name, NonEmptyString field)
        {
            if (name == string.Empty)
            {
                return GetFailResult((NonEmptyString)"{0} can't be empty", field);
            }

            const int max = 100;

            return name.Length > max ? GetFailResult((NonEmptyString)$"{{0}} can't be longer than {max} chars.", field) : GetOkResult(new Name(name));
        }
    }
}
