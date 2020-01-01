using System;
using DotNetTool.Service.ArgumentCheck;

namespace DotNetTool.Service.Extensions
{
    internal static class BoolExtensions
    {
        internal static bool IfFalseThen(this bool value, Action action)
        {
            Throw.IfNull(() => action);

            if (!value)
            {
                action();
            }

            return value;
        }

        internal static bool IsFalse(this bool source)
        {
            return !source;
        }

        internal static bool IfTrueThen(this bool value, Action action)
        {
            Throw.IfNull(() => action);

            if (value)
            {
                action();
            }

            return value;
        }

        internal static bool If(this bool value, Action action)
        {
            Throw.IfNull(() => action);

            return IfTrueThen(value, action);
        }

        internal static bool Else(this bool value, Action action)
        {
            Throw.IfNull(() => action);

            return IfFalseThen(value, action);
        }
    }
}
