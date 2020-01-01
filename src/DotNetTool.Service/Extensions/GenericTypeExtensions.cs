using System;
using System.Collections.Generic;
using System.Text;
using DotNetTool.Service.ArgumentCheck;

namespace DotNetTool.Service.Extensions
{
    public static class GenericTypeExtensions
    {
        private const double DEFAULT_DOUBLE_TOLERANCE = 0.000001;

        public static bool EqualsTo<T>(this T source, T target)
        {
            return EqualityComparer<T>.Default.Equals(source, target);
        }

        public static bool EqualsTo(this double source, double target)
        {
            return EqualsTo(source, target, DEFAULT_DOUBLE_TOLERANCE);
        }

        public static bool NotEqualsTo(this double source, double target)
        {
            return NotEqualsTo(source, target, DEFAULT_DOUBLE_TOLERANCE);
        }

        public static bool EqualsTo(this double source, double target, double tolerance)
        {
            return Math.Abs(source - target).IsLessThan(tolerance);
        }

        public static bool NotEqualsTo(this double source, double target, double tolerance)
        {
            return !source.EqualsTo(target, tolerance);
        }

        public static bool NotEqualsTo<T>(this T source, T target)
        {
            return !source.EqualsTo(target);
        }

        public static IList<T> ToIList<T>(this T item)
        {
            return new List<T> { item };
        }

        public static string ToString<T>(this T source, Dictionary<string, Func<T, object>> compiledExpressions)
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull(() => compiledExpressions);

            var stringBuilder = new StringBuilder();
            compiledExpressions.ForEach(item => stringBuilder.Append(item.ToString(source)));

            return stringBuilder.ToString();
        }
    }
}
