using System;
using DotNetTool.Service.ArgumentCheck;

namespace DotNetTool.Service.Extensions
{
    public static class ComparableExtensions
    {
        public static bool IsEqualTo<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return source.CompareTo(target) == 0;
        }

        public static bool IsLessThan<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return source.CompareTo(target) < 0;
        }

        public static bool IsLessOrEqual<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return !source.IsGreaterThan(target);
        }

        public static bool IsGreaterThan<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return source.CompareTo(target) > 0;
        }

        public static bool IsGreaterOrEqual<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return !source.IsLessThan(target);
        }

        public static bool IsInRange<T>(this T source, T lowerLimit, T upperLimit)
            where T : IComparable
        {
            Throw.IfLessThan(() => upperLimit, lowerLimit);

            return source.IsLessOrEqual(upperLimit) && source.IsGreaterOrEqual(lowerLimit);
        }
    }
}
