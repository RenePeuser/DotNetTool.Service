using System;
using System.Diagnostics;
using System.Globalization;

namespace DotNetTool.Service.ArgumentCheck
{
    internal static class Throw
    {
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
        internal static void IfNull<T>(Func<T> argumentFunc) where T : class
        {
            if (argumentFunc == null)
            {
                throw new ArgumentNullException(nameof(argumentFunc));
            }

            Throw.IfNullInternal(argumentFunc);
        }

        [DebuggerHidden]
        internal static void IfNullOrWhiteSpace(Func<string> argumentFunc)
        {
            Throw.IfNull(() => argumentFunc);
            Throw.IfNullInternal(argumentFunc);
            Throw.IfWhiteSpace(argumentFunc);
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
        private static void ThrowLessThanException<T>(Func<T> argumentFunc, T limit, Func<object, bool> predicate) where T : IComparable
        {
            throw new ArgumentOutOfRangeException(
                argumentFunc.GetParameterName(predicate),
                string.Format(CultureInfo.InvariantCulture, "Value: '{0}' must not be less than: '{1}'", argumentFunc(), limit));
        }
    }
}
