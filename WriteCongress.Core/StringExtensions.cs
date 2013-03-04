namespace System
{
    public static class StringExtensions
    {
        public static string Left(this string s, int number, bool emptyStringNulls = false)
        {
            if (s == null && emptyStringNulls)
            {
                return string.Empty;
            }
            else if (s == null)
            {
                return null;
            }
            if (s.Length < number)
            {
                return s;
            }
            return s.Substring(0, number);
        }

        public static string AsHexidecimal(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", String.Empty);
        }
    }
}