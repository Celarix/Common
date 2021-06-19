using System;
using System.Collections.Generic;
using System.Text;
// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ChrisAkridge.Common.Options
{
    public readonly struct Option<T> : IEquatable<Option<T>>, IComparable<Option<T>>
    {
        public bool HasValue { get; }
        internal T Value { get; }

        internal Option(T value, bool hasValue)
        {
            Value = value;
            HasValue = hasValue;
        }

        public bool Equals(Option<T> other)
        {
            if (!HasValue && !other.HasValue) { return true; }
            if (HasValue && other.HasValue) { return EqualityComparer<T>.Default.Equals(Value, other.Value); }

            return false;
        }

        public override bool Equals(object obj) => obj is Option<T> option && Equals(option);

        public static bool operator ==(Option<T> left, Option<T> right) => left.Equals(right);
        public static bool operator !=(Option<T> left, Option<T> right) => !left.Equals(right);

        public override int GetHashCode()
        {
            if (HasValue)
            {
                return (Value != null) ? Value.GetHashCode() : 1;
            }

            return 0;
        }

        public int CompareTo(Option<T> other)
        {
            if (HasValue && !other.HasValue) { return 1; }
            if (!HasValue && other.HasValue) { return -1; }

            return Comparer<T>.Default.Compare(Value, other.Value);
        }

        public static bool operator <(Option<T> left, Option<T> right) => left.CompareTo(right) < 0;
        public static bool operator <=(Option<T> left, Option<T> right) => left.CompareTo(right) <= 0;
        public static bool operator >(Option<T> left, Option<T> right) => left.CompareTo(right) > 0;
        public static bool operator >=(Option<T> left, Option<T> right) => left.CompareTo(right) >= 0;

        public override string ToString()
        {
            if (HasValue)
            {
                return Value == null ? "Some(null)" : $"Smome({Value})";
            }

            return "None";
        }

        public IEnumerable<T> ToEnumerable()
        {
            if (HasValue)
            {
                yield return Value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (HasValue)
            {
                yield return Value;
            }
        }

        public bool Contains(T value)
        {
            if (HasValue)
            {
                return (Value == null) ? value == null : Value.Equals(value);
            }

            return false;
        }

        public bool Exists(Func<T, bool> predicate)
        {
            if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }

            return HasValue && predicate(Value);
        }

        public T ValueOr(T alternative) => HasValue ? Value : alternative;

        public T ValueOr(Func<T> alternativeFactory)
        {
            if (alternativeFactory == null) { throw new ArgumentNullException(nameof(alternativeFactory)); }

            return HasValue ? Value : alternativeFactory();
        }

        public Option<T> Or(T alternative) => HasValue ? this : Option.Some(alternative);

        public Option<T> Or(Func<T> alternativeFactory)
        {
            if (alternativeFactory == null) { throw new ArgumentNullException(nameof(alternativeFactory)); }

            return HasValue ? this : Option.Some(alternativeFactory());
        }

        public Option<T> Else(Option<T> alternativeOption) => HasValue ? this : alternativeOption;

        public Option<T> Else(Func<Option<T>> alternativeOptionFactory)
        {
            if (alternativeOptionFactory == null) { throw new ArgumentNullException(nameof(alternativeOptionFactory)); }

            return HasValue ? this : alternativeOptionFactory();
        }

        public Option<T, TException> WithException<TException>(TException exception)
        {
            return Match(
                some: value => Option.Some<T, TException>(value),
                none: () => Option.None<T, TException>(exception)
            );
        }

        public Option<T, TException> WithException<TException>(Func<TException> exceptionFactory)
        {
            if (exceptionFactory == null) { throw new ArgumentNullException(nameof(exceptionFactory)); }

            return Match(
                some: value => Option.Some<T, TException>(value),
                none: () => Option.None<T, TException>(exceptionFactory())
            );
        }

        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            if (some == null) { throw new ArgumentNullException(nameof(some)); }
            if (none == null) { throw new ArgumentNullException(nameof(none)); }

            return HasValue ? some(Value) : none();
        }

        public void Match(Action<T> some, Action none)
        {
            if (some == null) { throw new ArgumentNullException(nameof(some)); }
            if (none == null) { throw new ArgumentNullException(nameof(none)); }

            if (HasValue) { some(Value); }
            else { none(); }
        }

        public void MatchSome(Action<T> some)
        {
            if (some == null) { throw new ArgumentNullException(nameof(some)); }

            if (HasValue) { some(Value); }
        }

        public void MatchNone(Action none)
        {
            if (none == null) { throw new ArgumentNullException(nameof(none)); }

            if (!HasValue) { none(); }
        }

        public Option<TResult> Map<TResult>(Func<T, TResult> mapping)
        {
            if (mapping == null) { throw new ArgumentNullException(nameof(mapping)); }

            return Match(
                some: value => Option.Some(mapping(value)),
                none: () => Option.None<TResult>()
            );
        }

        public Option<TResult> FlatMap<TResult>(Func<T, Option<TResult>> mapping)
        {
            if (mapping == null) { throw new ArgumentNullException(nameof(mapping)); }

            return Match(
                some: mapping,
                none: () => Option.None<TResult>()
            );
        }

        public Option<TResult> FlatMap<TResult, TException>(Func<T, Option<TResult, TException>> mapping)
        {
            if (mapping == null) { throw new ArgumentNullException(nameof(mapping)); }

            return FlatMap(value => mapping(value).WithoutException());
        }

        public Option<T> Filter(bool condition) => HasValue && !condition ? Option.None<T>() : this;

        public Option<T> Filter(Func<T, bool> predicate)
        {
            if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }

            return HasValue && !predicate(Value) ? Option.None<T>() : this;
        }

        public Option<T> NotNull() => HasValue && Value == null ? Option.None<T>() : this;
    }
}
