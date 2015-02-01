namespace AngleSharp.Performance
{
    using System;

    static class OutputExtensions
    {
        public static void WriteLine(this IOutput output)
        {
            output.Write(Environment.NewLine);
        }

        public static void WriteLine(this IOutput output, String text)
        {
            output.Write(text);
            output.WriteLine();
        }
    }
}
