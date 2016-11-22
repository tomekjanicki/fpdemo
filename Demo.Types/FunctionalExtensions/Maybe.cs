namespace Demo.Types.FunctionalExtensions
{
    using System;
    using NullGuard;

    public struct Maybe<T> : IEquatable<Maybe<T>>
        where T : class
    {
        private readonly T _value;

        private Maybe([AllowNull]T value)
        {
            _value = value;
        }

        public T Value
        {
            get
            {
                if (HasNoValue)
                {
                    throw new InvalidOperationException();
                }

                return _value;
            }
        }

        public bool HasValue => _value != null;

        public bool HasNoValue => !HasValue;

        public static implicit operator Maybe<T>([AllowNull]T value)
        {
            return new Maybe<T>(value);
        }

        public static bool operator ==(Maybe<T> maybe, T value)
        {
            return !maybe.HasNoValue && maybe.Value.Equals(value);
        }

        public static bool operator !=(Maybe<T> maybe, T value)
        {
            return !(maybe == value);
        }

        public static bool operator ==(Maybe<T> first, Maybe<T> second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Maybe<T> first, Maybe<T> second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            if (obj is T)
            {
                obj = new Maybe<T>((T)obj);
            }

            if (!(obj is Maybe<T>))
            {
                return false;
            }

            var other = (Maybe<T>)obj;
            return Equals(other);
        }

        public bool Equals(Maybe<T> other)
        {
            if (HasNoValue && other.HasNoValue)
            {
                return true;
            }

            return !HasNoValue && !other.HasNoValue && _value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            return HasNoValue ? 0 : _value.GetHashCode();
        }

        public override string ToString()
        {
            return HasNoValue ? "No value" : Value.ToString();
        }
    }
}
