using System;
using System.Collections.Generic;
using System.Text;
// ReSharper disable CompareNonConstrainedGenericWithNull

namespace ChrisAkridge.Common.Options
{
    public static class OptionExtensions
    {
        public static Option<T> Some<T>(this T value) => Option.Some(value);
        public static Option<T, TException> Some<T, TException>(this T value) => Option.Some<T, TException>(value);
        public static Option<T> None<T>(this T value) => Option.None<T>();
        public static Option<T, TException> None<T, TException>(this T value, TException exception) =>
            Option.None<T, TException>(exception);

        public static Option<T> SomeWhen<T>(this T value, Func<T, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return predicate(value) ? Option.Some(value) : Option.None<T>();
        }

        public static Option<T, TException> SomeWhen<T, TException>(this T value, Func<T, bool> predicate,
                                                                    TException exception)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return predicate(value) ? Option.Some<T, TException>(value) : Option.None<T, TException>(exception);
        }

        public static Option<T, TException> SomeWhen<T, TException>(this T value, Func<T, bool> predicate,
                                                                    Func<TException> exceptionFactory)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (exceptionFactory == null) throw new ArgumentNullException(nameof(exceptionFactory));

            return predicate(value)
                ? Option.Some<T, TException>(value)
                : Option.None<T, TException>(exceptionFactory());
        }

        public static Option<T> NoneWhen<T>(this T value, Func<T, bool> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return value.SomeWhen(val => !predicate(val));
        }

        public static Option<T, TException> NoneWhen<T, TException>(this T value, Func<T, bool> predicate,
                                                                    TException exception)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return value.SomeWhen(val => !predicate(val), exception);
        }

        public static Option<T, TException> NoneWhen<T, TException>(this T value, Func<T, bool> predicate,
                                                                    Func<TException> exceptionFactory)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (exceptionFactory == null) throw new ArgumentNullException(nameof(exceptionFactory));

            return value.SomeWhen(val => !predicate(val), exceptionFactory);
        }

        public static Option<T> SomeNotNull<T>(this T value) => value.SomeWhen(val => val != null);

        public static Option<T, TException> SomeNotNull<T, TException>(this T value, TException exception) =>
            value.SomeWhen(val => val != null, exception);

        public static Option<T, TException> SomeNotNull<T, TException>(this T value, Func<TException> exceptionFactory)
        {
            if (exceptionFactory == null) throw new ArgumentNullException(nameof(exceptionFactory));

            return value.SomeWhen(val => val != null, exceptionFactory);
        }

        public static Option<T> ToOption<T>(this T? value) where T : struct =>
            value.HasValue ? Option.Some(value.Value) : Option.None<T>();
        
        public static Option<T, TException> ToOption<T, TException>(this T? value, TException exception)
            where T : struct =>
            value.HasValue ? Option.Some<T, TException>(value.Value) : Option.None<T, TException>(exception);

        public static Option<T, TException> ToOption<T, TException>(this T? value, Func<TException> exceptionFactory)
            where T : struct
        {
            if (exceptionFactory == null) throw new ArgumentNullException(nameof(exceptionFactory));

            return value.HasValue
                ? Option.Some<T, TException>(value.Value)
                : Option.None<T, TException>(exceptionFactory());
        }

        public static T ValueOrException<T>(this Option<T, T> option) =>
            option.HasValue ? option.Value : option.Exception;

        public static Option<T> Flatten<T>(this Option<Option<T>> option) => option.FlatMap(innerOption => innerOption);

        public static Option<T, TException> Flatten<T, TException>(
            this Option<Option<T, TException>, TException> option) =>
            option.FlatMap(innerOption => innerOption);
    }
}
