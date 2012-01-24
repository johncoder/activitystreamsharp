using System.Globalization;

namespace ActivityStreamSharp.Utilities
{
    internal static class StringUtilities
    {
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (!char.IsUpper(s[0]))
                return s;

            string camelCase = char.ToLower(s[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            if (s.Length > 1)
                camelCase += s.Substring(1);

            return camelCase;
        }
    }
}