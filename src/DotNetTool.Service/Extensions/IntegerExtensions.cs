using System;

namespace DotNetTool.Service.Extensions
{
    internal static class IntegerExtensions
    {
        internal static double DivideBy(this int value, double divisor)
        {
            if (divisor.IsZero())
            {
                throw new ArgumentException("Division by 0 is not allowed");
            }

            if (divisor.IsNan())
            {
                throw new ArgumentException("Divisor is not a number");
            }

            return value / divisor;
        }

        internal static int MultiplyBy(this int value, int multiplier)
        {
            return value * multiplier;
        }

        internal static int Plus(this int value, int addend)
        {
            return value + addend;
        }

        internal static double ToDouble(this int value)
        {
            return value;
        }
    }
}
