namespace AngleSharp.Performance
{
    using System;

    static class StringExtensions
    {
        public static String Center(this String str, Int32 width)
        {
            if (str.Length > width)
                return str.Substring(0, width - 3) + "...";

            return str.PadLeft((width + str.Length) / 2).PadRight(width);
        }

        public static String Left(this String str, Int32 width)
        {
            if (str.Length > width)
                return str.Substring(0, width - 3) + "...";

            return str.PadRight(width);
        }

        public static String Right(this String str, Int32 width)
        {
            if (str.Length > width)
                return "..." + str.Substring(str.Length - width + 3);

            return str.PadLeft(width);
        }
    }
}
