namespace AngleSharp.Css.Parser
{
    using AngleSharp.Text;
    using System;
    using System.Globalization;

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

        /// <summary>
        /// Consumes the escape sequence if any. Assumes, the source currently being at a
        /// solidus (valid escape).
        /// </summary>
        public static String ConsumeEscape(this StringSource source)
        {
            var current = source.Next();

            if (current.IsHex())
            {
                var isHex = true;
                var escape = new Char[6];
                var length = 0;

                while (isHex && length < escape.Length)
                {
                    escape[length++] = current;
                    current = source.Next();
                    isHex = current.IsHex();
                }

                if (!current.IsSpaceCharacter())
                {
                    source.Back();
                }

                var code = 0;
                var pwr = 1;

                for (var i = length - 1; i >= 0; i--)
                {
                    code += escape[i].FromHex() * pwr;
                    pwr *= 16;
                }

                if (!code.IsInvalid())
                {
                    return Char.ConvertFromUtf32(code);
                }

                current = Symbols.Replacement;
            }

            return current.ToString();
        }

        /// <summary>
        /// Checks if the current position holds a valid escape.
        /// </summary>
        public static Boolean IsValidEscape(this StringSource source)
        {
            var current = source.Current;

            if (current == Symbols.ReverseSolidus)
            {
                current = source.Peek();
                return current != Symbols.EndOfFile && !current.IsLineBreak();
            }

            return false;
        }
    }
}
