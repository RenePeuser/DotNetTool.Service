using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using DotNetTool.Service.Extensions;

namespace DotNetTool.Service.ArgumentCheck
{
    internal static class Throw
    {
        [DebuggerHidden]
        internal static void IfGreaterThan<T>(Func<T> argumentFunc, T limit) where T : IComparable
        {
            Throw.IfNull(() => argumentFunc);

            if (!argumentFunc().IsGreaterThan(limit))
            {
                return;
            }

            ThrowGreaterThanException(argumentFunc, limit, arg => arg is T && ((T)arg).IsGreaterThan(limit));
        }

        [DebuggerHidden]
        internal static void IfGreaterOrEqual<T>(Func<T> argumentFunc, T limit) where T : IComparable
        {
            Throw.IfNull(() => argumentFunc);

            if (!argumentFunc().IsGreaterOrEqual(limit))
            {
                return;
            }

            ThrowGreaterOrEqualException(argumentFunc, limit, arg => arg is T && ((T)arg).IsGreaterOrEqual(limit));
        }

        [DebuggerHidden]
        internal static void IfLessThan<T>(Func<T> argumentFunc, T limit) where T : IComparable
        {
            Throw.IfNull(() => argumentFunc);

            if (!argumentFunc().IsLessThan(limit))
            {
                return;
            }

            ThrowLessThanException(argumentFunc, limit, arg => arg is T && ((T)arg).IsLessThan(limit));
        }

        [DebuggerHidden]
        internal static void IfLessOrEqual<T>(Func<T> argumentFunc, T limit) where T : IComparable
        {
            Throw.IfNull(() => argumentFunc);

            if (!argumentFunc().IsLessOrEqual(limit))
            {
                return;
            }

            ThrowLessOrEqualException(argumentFunc, limit, arg => arg is T && ((T)arg).IsLessOrEqual(limit));
        }


        [DebuggerHidden]
        internal static void IfNull<T>(Func<T> argumentFunc) where T : class
        {
            if (argumentFunc == null)
            {
                throw new ArgumentNullException(nameof(argumentFunc));
            }

            Throw.IfNullInternal(argumentFunc);
        }

        [DebuggerHidden]
        internal static void IfNullOrEmpty<T>(Func<IEnumerable<T>> argumentFunc)
        {
            Throw.IfNull(() => argumentFunc);
            Throw.IfNullInternal(argumentFunc);
            Throw.IfEmpty(argumentFunc);
        }

        [DebuggerHidden]
        internal static void IfNullOrAny<T>(Func<IEnumerable<T>> argumentFunc, Func<T, bool> predicate)
        {
            Throw.IfNull(() => argumentFunc);
            Throw.IfNull(() => predicate);

            Throw.IfNullInternal(argumentFunc);

            if (!argumentFunc().Any(predicate))
            {
                return;
            }

            throw new ArgumentException(
                string.Format(CultureInfo.InvariantCulture, "The sequence must not contain any items that satisfy the predicate '{0}'.", predicate.GetMethodInfo().Name),
                argumentFunc.GetParameterName(x => x.Is<IEnumerable<T>>() && x.Cast<IEnumerable<T>>().Any(predicate)));
        }

        [DebuggerHidden]
        internal static void IfNullOrEmpty(Func<string> argumentFunc)
        {
            Throw.IfNull(() => argumentFunc);
            Throw.IfNullInternal(argumentFunc);
            Throw.IfEmpty(argumentFunc);
        }

        [DebuggerHidden]
        internal static void IfNullOrWhiteSpace(Func<string> argumentFunc)
        {
            Throw.IfNull(() => argumentFunc);
            Throw.IfNullInternal(argumentFunc);
            Throw.IfWhiteSpace(argumentFunc);
        }

        [DebuggerHidden]
        internal static void IfEqualsTo<T>(Func<T> argumentFunc, T expectedValue)
        {
            Throw.IfNull(() => argumentFunc);
            Throw.IfEqualsToInternal(argumentFunc, expectedValue);
        }

        [DebuggerHidden]
        internal static void IfNotEqualsTo<T>(Func<T> argumentFunc, T expectedValue)
        {
            IfNull(() => argumentFunc);

            Throw.IfNotEqualsToInternal(argumentFunc, expectedValue);
        }

        [DebuggerHidden]
        internal static void IfLengthIsNot(Func<string> argumentFunc, int length)
        {
            Throw.IfNull(() => argumentFunc);

            if (!argumentFunc().Length.NotEqualsTo(length))
            {
                return;
            }

            ThrowStringLengthIsNotException(argumentFunc, length, arg => arg.Is<string>() && arg.Cast<string>().Length.NotEqualsTo(length));
        }


        [DebuggerHidden]
        private static void IfNullOrWhiteSpace(Func<char> argumentFunc)
        {
            Throw.IfNull(() => argumentFunc);

            Throw.IfWhiteSpace(argumentFunc);
        }

        [DebuggerHidden]
        private static void IfEmpty<T>(Func<IEnumerable<T>> argument)
        {
            if (argument().Any())
            {
                return;
            }

            throw new ArgumentException(
                "The enumerable must not be empty.",
                argument.GetParameterName(arg => arg.Is<IEnumerable<T>>() && arg.Cast<IEnumerable<T>>().IsEmpty()));
        }

        [DebuggerHidden]
        private static void IfNullInternal<T>(Func<T> argumentFunc)
            where T : class
        {
            if (argumentFunc() != null)
            {
                return;
            }

            throw new ArgumentNullException(argumentFunc.GetParameterName(arg => arg == null));
        }

        [DebuggerHidden]
        private static void IfWhiteSpace(Func<string> argument)
        {
            if (!argument().IsNullOrWhiteSpace())
            {
                return;
            }

            throw new ArgumentException(
                "The string must not be a whitespace.",
                argument.GetParameterName(arg => arg.Is<string>() && arg.Cast<string>().IsNullOrWhiteSpace()));
        }

        [DebuggerHidden]
        private static void IfWhiteSpace(Func<char> argument)
        {
            if (!char.IsWhiteSpace(argument()))
            {
                return;
            }

            throw new ArgumentException(
                "The char must not be a whitespace.",
                argument.GetParameterName(arg => arg.Is<char>() && char.IsWhiteSpace(arg.Cast<char>())));
        }

        [DebuggerHidden]
        private static void ThrowGreaterThanException<T>(Func<T> argumentFunc, T limit, Func<object, bool> predicate) where T : IComparable
        {
            throw new ArgumentOutOfRangeException(
                argumentFunc.GetParameterName(predicate),
                string.Format(CultureInfo.InvariantCulture, "Value: '{0}' must not be greater than: '{1}'", argumentFunc(), limit));
        }

        [DebuggerHidden]
        private static void ThrowGreaterOrEqualException<T>(Func<T> argumentFunc, T limit, Func<object, bool> predicate) where T : IComparable
        {
            throw new ArgumentOutOfRangeException(
                argumentFunc.GetParameterName(predicate),
                string.Format(CultureInfo.InvariantCulture, "Value: '{0}' must not be greater than or equal to: '{1}'", argumentFunc(), limit));
        }

        [DebuggerHidden]
        private static void ThrowLessThanException<T>(Func<T> argumentFunc, T limit, Func<object, bool> predicate) where T : IComparable
        {
            throw new ArgumentOutOfRangeException(
                argumentFunc.GetParameterName(predicate),
                string.Format(CultureInfo.InvariantCulture, "Value: '{0}' must not be less than: '{1}'", argumentFunc(), limit));
        }

        [DebuggerHidden]
        private static void ThrowLessOrEqualException<T>(Func<T> argumentFunc, T limit, Func<object, bool> predicate) where T : IComparable
        {
            throw new ArgumentOutOfRangeException(
                argumentFunc.GetParameterName(predicate),
                string.Format(CultureInfo.InvariantCulture, "Value: '{0}' must not be less than or equal to: '{1}'", argumentFunc(), limit));
        }

        [DebuggerHidden]
        private static void ThrowStringLengthIsNotException(Func<string> argumentFunc, int length, Func<object, bool> predicate)
        {
            throw new ArgumentOutOfRangeException(
                argumentFunc.GetParameterName(predicate),
                string.Format(CultureInfo.InvariantCulture, "String length: '{0}' is not as expected. Expected value: '{1}", argumentFunc(), length));
        }

        [DebuggerHidden]
        private static void ThrowIsAnyItemNullException<T>(Func<IEnumerable<T>> argumentFunc, Func<object, bool> predicate)
        {
            throw new ArgumentNullException(
                argumentFunc.GetParameterName(predicate),
                string.Format(CultureInfo.InvariantCulture, "At least one item in the enumeration '{0}' was null.", argumentFunc()));
        }

        [DebuggerHidden]
        private static void ThrowValueOutOfRangeException<T>(Func<T> argumentFunc, T minimum, T maximum, Func<object, bool> predicate) where T : IComparable
        {
            throw new ArgumentOutOfRangeException(
                argumentFunc.GetParameterName(predicate),
                string.Format(CultureInfo.InvariantCulture, "Value: '{0}' is not in expected range from '{1}' to '{2}'", argumentFunc(), minimum, maximum));
        }

        [DebuggerHidden]
        private static void IfNotEqualsToInternal<T>(Func<T> argumentFunc, T expectedValue)
        {
            var argumentValue = argumentFunc();
            if (!argumentValue.NotEqualsTo(expectedValue))
            {
                return;
            }

            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The argument value: '{0}' is not equal to: {1}", argumentValue, expectedValue), argumentFunc.GetParameterName(arg => arg.NotEqualsTo(expectedValue)));
        }

        [DebuggerHidden]
        private static void IfEqualsToInternal<T>(Func<T> argumentFunc, T expectedValue)
        {
            var argumentValue = argumentFunc();
            if (!argumentValue.EqualsTo(expectedValue))
            {
                return;
            }

            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The argument value: '{0}' must not be equal to: {1}", argumentValue, expectedValue), argumentFunc.GetParameterName(arg => arg.EqualsTo(expectedValue)));
        }
    }
}
