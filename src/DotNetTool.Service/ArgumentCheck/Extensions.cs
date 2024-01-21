using System;
using System.Diagnostics;

namespace DotNetTool.Service
{
    public static class ArgumentCheckExtensions
    {
        [DebuggerHidden]
        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        [DebuggerHidden]
        public static T Cast<T>(this object source) where T : class
        {
            return (T)source;
        }

        [DebuggerHidden]
        public static bool Is<T>(this object source)
        {
            return source is T;
        }

        [DebuggerHidden]
        internal static bool IsLessThan<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return source.CompareTo(target) < 0;
        }
    }
}
