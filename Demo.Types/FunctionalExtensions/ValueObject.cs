namespace Demo.Types.FunctionalExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            var valueObject = obj as T;

            return !ReferenceEquals(valueObject, null) && EqualsCore(valueObject);
        }

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected static T GetValue(Func<IResult<T, NonEmptyString>> resultFunc)
        {
            var result = resultFunc();
            result.EnsureIsNotFaliure();
            return result.Value;
        }

        protected static IResult<T, NonEmptyString> GetFailResult(NonEmptyString message, NonEmptyString field)
        {
            return ((NonEmptyString)string.Format(message, field)).GetFailResult<T>();
        }

        protected static IResult<T, NonEmptyString> GetFailResult(NonEmptyString message)
        {
            return message.GetFailResult<T>();
        }

        protected static IResult<T, NonEmptyString> GetOkResult(T value)
        {
            return value.GetOkMessage();
        }

        protected abstract bool EqualsCore(T other);

        protected abstract int GetHashCodeCore();

        protected int GetCalculatedHashCode(IEnumerable<object> list)
        {
            return list.Aggregate(13, (current, i) => current * 7 + i.GetHashCode());
        }
    }
}
