using System;
using DotNetTool.Service.ArgumentCheck;

namespace DotNetTool.Service.Extensions
{
    internal static class ComparableExtensions
    {
        internal static bool IsEqualTo<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return source.CompareTo(target) == 0;
        }

        internal static bool IsLessThan<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return source.CompareTo(target) < 0;
        }

        internal static bool IsLessOrEqual<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return !source.IsGreaterThan(target);
        }

        internal static bool IsGreaterThan<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return source.CompareTo(target) > 0;
        }

        internal static bool IsGreaterOrEqual<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return !source.IsLessThan(target);
        }

        internal static bool IsInRange<T>(this T source, T lowerLimit, T upperLimit)
            where T : IComparable
        {
            Throw.IfLessThan(() => upperLimit, lowerLimit);

            return source.IsLessOrEqual(upperLimit) && source.IsGreaterOrEqual(lowerLimit);
        }
    }
}
