namespace AngleSharp.Text
{
    using System;

    /// <summary>
    /// Extensions for the string source helper.
    /// </summary>
    public static class StringSourceExtensions
    {
        /// <summary>
        /// Skips all spaces starting at the current character.
        /// </summary>
        public static Char SkipSpaces(this StringSource source)
        {
            var current = source.Current;

            while (current.IsSpaceCharacter())
            {
                current = source.Next();
            }

            return current;
        }

        /// <summary>
        /// Goes back n characters.
        /// </summary>
        public static Char Next(this StringSource source, Int32 n)
        {
            for (var i = 0; i < n; i++)
            {
                source.Next();
            }

            return source.Current;
        }

        /// <summary>
        /// Goes back n characters.
        /// </summary>
        public static Char Back(this StringSource source, Int32 n)
        {
            for (var i = 0; i < n; i++)
            {
                source.Back();
            }

            return source.Current;
        }

        /// <summary>
        /// Gets the upcoming character without advancing.
        /// </summary>
        public static Char Peek(this StringSource source)
        {
            var c = source.Next();
            source.Back();
            return c;
        }
    }
}
