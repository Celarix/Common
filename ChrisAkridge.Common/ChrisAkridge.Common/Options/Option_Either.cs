using System;
using System.Collections.Generic;
// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ChrisAkridge.Common.Options
{
    public readonly struct Option<T, TException> : IEquatable<Option<T, TException>>, IComparable<Option<T, TException>>
    {
        public bool HasValue { get; }

        internal T Value { get; }
        internal TException Exception { get; }

        internal Option(T value, TException exception, bool hasValue)
        {
            Value = value;
            Exception = exception;
            HasValue = hasValue;
        }

        public bool Equals(Option<T, TException> other)
        {
            if (!HasValue && !other.HasValue)
            {
                return EqualityComparer<TException>.Default.Equals(Exception, other.Exception);
            }

            if (HasValue && other.HasValue)
            {
                return EqualityComparer<T>.Default.Equals(Value, other.Value);
            }

            return false;
        }

        public override bool Equals(object obj) => obj is Option<T, TException> option && Equals(option);

        public static bool operator ==(Option<T, TException> left, Option<T, TException> right) => left.Equals(right);
        public static bool operator !=(Option<T, TException> left, Option<T, TException> right) => !left.Equals(right);

        public override int GetHashCode()
        {
            if (HasValue)
            {
                return (Value != null) ? Value.GetHashCode() : 1;
            }

            return (Exception != null) ? Exception.GetHashCode() : 0;
        }

        public int CompareTo(Option<T, TException> other)
        {
            if (HasValue && !other.HasValue) { return 1; }
            if (!HasValue && other.HasValue) { return -1; }

            return HasValue
                ? Comparer<T>.Default.Compare(Value, other.Value)
                : Comparer<TException>.Default.Compare(Exception, other.Exception);
        }

        public static bool operator <(Option<T, TException> left, Option<T, TException> right) => left.CompareTo(right) < 0;
        public static bool operator <=(Option<T, TException> left, Option<T, TException> right) => left.CompareTo(right) <= 0;
        public static bool operator >(Option<T, TException> left, Option<T, TException> right) => left.CompareTo(right) > 0;
        public static bool operator >=(Option<T, TException> left, Option<T, TException> right) => left.CompareTo(right) >= 0;

        public override string ToString()
        {
            if (HasValue)
            {
                return (Value != null) ? $"Some({Value})" : "Some(null)";
            }

            return (Exception != null) ? $"None({Exception})" : "None(null)";
        }

        public IEnumerable<T> ToEnumerable()
        {
            if (HasValue) { yield return Value; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (HasValue) { yield return Value; }
        }

        public bool ValueIs(T searchValue)
        {
            if (HasValue)
            {
                return (Value != null) ? Value.Equals(searchValue) : searchValue == null;
            }

            return false;
        }

        public bool ValueMatches(Func<T, bool> predicate)
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

        public T ValueOr(Func<TException, T> alternativeFactory)
        {
            if (alternativeFactory == null) { throw new ArgumentNullException(nameof(alternativeFactory)); }
            return HasValue ? Value : alternativeFactory(Exception);
        }

        public Option<T, TException> Or(T alternative) => HasValue ? this : Option.Some<T, TException>(alternative);

        public Option<T, TException> Or(Func<T> alternativeFactory)
        {
            if (alternativeFactory == null) { throw new ArgumentNullException(nameof(alternativeFactory)); }
            return HasValue ? this : Option.Some<T, TException>(alternativeFactory());
        }

        public Option<T, TException> Or(Func<TException, T> alternativeFactory)
        {
            if (alternativeFactory == null) { throw new ArgumentNullException(nameof(alternativeFactory)); }
            return HasValue ? this : Option.Some<T, TException>(alternativeFactory(Exception));
        }

        public Option<T, TException> Else(Option<T, TException> alternativeOption) => HasValue ? this : alternativeOption;

        public Option<T, TException> Else(Func<Option<T, TException>> alternativeOptionFactory)
        {
            if (alternativeOptionFactory == null) { throw new ArgumentNullException(nameof(alternativeOptionFactory)); }
            return HasValue ? this : alternativeOptionFactory();
        }

        public Option<T, TException> Else(Func<TException, Option<T, TException>> alternativeOptionFactory)
        {
            if (alternativeOptionFactory == null) { throw new ArgumentNullException(nameof(alternativeOptionFactory)); }
            return HasValue ? this : alternativeOptionFactory(Exception);
        }

        public Option<T> WithoutException()
        {
            return Match(
                some: value => Option.Some(value),
                none: _ => Option.None<T>());
        }

        public TResult Match<TResult>(Func<T, TResult> some, Func<TException, TResult> none)
        {
            if (some == null) { throw new ArgumentNullException(nameof(some)); }
            if (none == null) { throw new ArgumentNullException(nameof(none)); }
            return HasValue ? some(Value) : none(Exception);
        }

        public void Match(Action<T> some, Action<TException> none)
        {
            if (some == null) { throw new ArgumentNullException(nameof(some)); }
            if (none == null) { throw new ArgumentNullException(nameof(none)); }

            if (HasValue) { some(Value); }
            else { none(Exception); }
        }

        public void MatchSome(Action<T> some)
        {
            if (some == null) { throw new ArgumentNullException(nameof(some)); }

            if (HasValue) { some(Value); }
        }

        public void MatchNone(Action<TException> none)
        {
            if (none == null) { throw new ArgumentNullException(nameof(none)); }

            if (!HasValue) { none(Exception); }
        }

        public Option<TResult, TException> Map<TResult>(Func<T, TResult> mapping)
        {
            if (mapping == null) { throw new ArgumentNullException(nameof(mapping)); }

            return Match(
                some: value => Option.Some<TResult, TException>(mapping(value)),
                none: exception => Option.None<TResult, TException>(exception)
            );
        }

        public Option<T, TExceptionResult> MapException<TExceptionResult>(Func<TException, TExceptionResult> mapping)
        {
            if (mapping == null) throw new ArgumentNullException(nameof(mapping));

            return Match(
                some: value => Option.Some<T, TExceptionResult>(value),
                none: exception => Option.None<T, TExceptionResult>(mapping(exception))
            );
        }

        public Option<TResult, TException> FlatMap<TResult>(Func<T, Option<TResult, TException>> mapping)
        {
            if (mapping == null) { throw new ArgumentNullException(nameof(mapping)); }

            return Match(
                some: mapping,
                none: exception => Option.None<TResult, TException>(exception)
            );
        }

        public Option<TResult, TException> FlatMap<TResult>(Func<T, Option<TResult>> mapping, TException exception)
        {
            if (mapping == null) { throw new ArgumentNullException(nameof(mapping)); }

            return FlatMap(value => mapping(value).WithException(exception));
        }

        public Option<TResult, TException> FlatMap<TResult>(Func<T, Option<TResult>> mapping,
          Func<TException> exceptionFactory)
        {
            if (mapping == null) { throw new ArgumentNullException(nameof(mapping)); }
            if (exceptionFactory == null) { throw new ArgumentNullException(nameof(exceptionFactory)); }

            return FlatMap(value => mapping(value).WithException(exceptionFactory));
        }

        public Option<T, TException> Filter(bool condition, TException exception) =>
            HasValue && !condition ? Option.None<T, TException>(exception) : this;

        public Option<T, TException> Filter(bool condition, Func<TException> exceptionFactory)
        {
            if (exceptionFactory == null) throw new ArgumentNullException(nameof(exceptionFactory));

            return HasValue && !condition ? Option.None<T, TException>(exceptionFactory()) : this;
        }

        public Option<T, TException> Filter(Func<T, bool> predicate, TException exception)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return HasValue && !predicate(Value) ? Option.None<T, TException>(exception) : this;
        }

        public Option<T, TException> Filter(Func<T, bool> predicate, Func<TException> exceptionFactory)
        {
            if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }
            if (exceptionFactory == null) { throw new ArgumentNullException(nameof(exceptionFactory)); }

            return HasValue && !predicate(Value) ? Option.None<T, TException>(exceptionFactory()) : this;
        }

        public Option<T, TException> NotNull(TException exception) =>
            HasValue && Value == null ? Option.None<T, TException>(exception) : this;

        public Option<T, TException> NotNull(Func<TException> exceptionFactory)
        {
            if (exceptionFactory == null) { throw new ArgumentNullException(nameof(exceptionFactory)); }

            return HasValue && Value == null ? Option.None<T, TException>(exceptionFactory()) : this;
        }
    }
}
