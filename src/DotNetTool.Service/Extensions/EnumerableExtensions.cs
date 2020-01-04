using System.Collections.Generic;
using System.Linq;
using DotNetTool.Service.ArgumentCheck;

namespace DotNetTool.Service.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static IEnumerable<string> FilterNullOrWhitespace(this IEnumerable<string> source)
        {
            Throw.IfNull(() => source);

            return source.Where(s => s.IsNotNullOrWhiteSpace());
        }
    }
}
