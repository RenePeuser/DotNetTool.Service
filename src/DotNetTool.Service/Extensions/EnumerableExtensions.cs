using System.Collections.Generic;
using System.Linq;

namespace DotNetTool.Service
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
