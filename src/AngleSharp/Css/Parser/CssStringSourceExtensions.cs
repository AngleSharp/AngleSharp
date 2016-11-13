namespace AngleSharp.Css.Parser
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Contains some useful extension methods for the StringSource
    /// from the perspective of the CSS micro parsers.
    /// </summary>
    public static class CssStringSourceExtensions
    {
        /// <summary>
        /// Skips all characters to the end of a CSS comment. Assumes the
        /// start of a CSS comment has been seen.
        /// </summary>
        public static Char SkipCssComment(this StringSource source)
        {
            var current = source.Next();

            while (current != Symbols.EndOfFile)
            {
                if (current == Symbols.Asterisk)
                {
                    current = source.Next();

                    if (current == Symbols.Solidus)
                    {
                        return source.Next();
                    }
                }
                else
                {
                    current = source.Next();
                }
            }

            return current;
        }
    }
}
