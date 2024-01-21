using System.Collections.Generic;

namespace DotNetTool.Service
{
    internal static class GenericTypeExtensions
    {
        internal static bool EqualsTo<T>(this T source, T target)
        {
            return EqualityComparer<T>.Default.Equals(source, target);
        }
    }
}
