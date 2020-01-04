namespace DotNetTool.Service.Extensions
{
    internal static class ObjectExtensions
    {
        internal static T As<T>(this object source)
        {
            var result = default(T);

            if (source is T)
            {
                result = (T)source;
            }

            return result;
        }

        internal static T Cast<T>(this object source)
        {
            return (T)source;
        }

        internal static bool Is<T>(this object source)
        {
            return source is T;
        }

        internal static bool IsNotNull(this object source)
        {
            return !source.EqualsTo(null);
        }

        internal static bool IsNull(this object source)
        {
            return source.EqualsTo(null);
        }
    }
}
