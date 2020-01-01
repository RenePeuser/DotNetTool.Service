using System;

namespace DotNetTool.Service.Extensions
{
    internal static class DoubleExtensions
    {
        internal static bool IsNan(this double value)
        {
            return double.IsNaN(value);
        }

        internal static bool IsZero(this double source)
        {
            var result = source.EqualsTo(default);

            return result;
        }

        internal static int Ceiling(this double value)
        {
            return (int) Math.Ceiling(value);
        }
    }
}
