namespace DotNetTool.Service
{
    internal static class StringExtensions
    {
        internal static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        internal static bool IsNotNullOrEmpty(this string source)
        {
            return !source.IsNullOrEmpty();
        }

        internal static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        internal static bool IsNotNullOrWhiteSpace(this string source)
        {
            return IsNullOrWhiteSpace(source).IsFalse();
        }
    }
}
