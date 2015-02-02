namespace AngleSharp.Extensions
{
    using System;
    using System.Diagnostics;
#if !LEGACY
    using System.Runtime.CompilerServices;
#endif

    /// <summary>
    /// Useful methods for chars.
    /// </summary>
    [DebuggerStepThrough]
    static class CharExtensions
    {
        /// <summary>
        /// Converts a given character from the hex representation (0-9A-Fa-f)
        /// to an integer.
        /// </summary>
        /// <param name="c">The character to convert.</param>
        /// <returns>
        /// The integer value or undefined behavior if invalid.
        /// </returns>
        public static Int32 FromHex(this Char c)
        {
            return c.IsDigit() ? c - 0x30 : c - (c.IsLowercaseAscii() ? 0x57 : 0x37);
        }

        /// <summary>
        /// Transforms the given number to a hexadecimal string.
        /// </summary>
        /// <param name="num">The number (0-255).</param>
        /// <returns>A 2 digit upper case hexadecimal string.</returns>
        public static String ToHex(this Byte num)
        {
            var chrs = new Char[2];
            var rem = num >> 4;
            chrs[0] = (Char)(rem + (rem < 10 ? 48 : 55));
            rem = num - 16 * rem;
            chrs[1] = (Char)(rem + (rem < 10 ? 48 : 55));
            return new String(chrs);
        }

        /// <summary>
        /// Transforms the given character to a hexadecimal string.
        /// </summary>
        /// <param name="character">The single character.</param>
        /// <returns>A minimal digit lower case hexadecimal string.</returns>
        public static String ToHex(this Char character)
        {
            return ((Int32)character).ToString("x");
        }

        /// <summary>
        /// Determines if the given character is in the given range.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <param name="lower">The lower bound of the range.</param>
        /// <param name="upper">The upper bound of the range.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInRange(this Char c, Int32 lower, Int32 upper)
        {
            return c >= lower && c <= upper;
        }

        /// <summary>
        /// Determines if the given character is allowed as-it-is in queries.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNormalQueryCharacter(this Char c)
        {
            return c.IsInRange(0x21, 0x7e) && c != Symbols.DoubleQuote &&
                c != Symbols.CurvedQuote && c != Symbols.Num &&
                c != Symbols.LessThan && c != Symbols.GreaterThan;
        }

        /// <summary>
        /// Determines if the given character is allowed as-it-is in paths.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNormalPathCharacter(this Char c)
        {
            return c.IsInRange(0x20, 0x7e) && c != Symbols.DoubleQuote &&
                c != Symbols.CurvedQuote && c != Symbols.Num &&
                c != Symbols.LessThan && c != Symbols.GreaterThan && 
                c != Symbols.Space && c != Symbols.QuestionMark;
        }

        /// <summary>
        /// Determines if the given character is a uppercase character (A-Z) as
        /// specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#uppercase-ascii-letters
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUppercaseAscii(this Char c)
        {
            return c >= 0x41 && c <= 0x5a;
        }

        /// <summary>
        /// Determines if the given character is a lowercase character (a-z) as
        /// specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#lowercase-ascii-letters
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLowercaseAscii(this Char c)
        {
            return c >= 0x61 && c <= 0x7a;
        }

        /// <summary>
        /// Determines if the given character is a alphanumeric character
        /// (0-9a-zA-z) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#alphanumeric-ascii-characters
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAlphanumericAscii(this Char c)
        {
            return c.IsDigit() || c.IsUppercaseAscii() || c.IsLowercaseAscii();
        }

        /// <summary>
        /// Determines if the given character is a hexadecimal (0-9a-fA-F) as
        /// specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#ascii-hex-digits
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsHex(this Char c)
        {
            return c.IsDigit() || (c >= 0x41 && c <= 0x46) || (c >= 0x61 && c <= 0x66);
        }

        /// <summary>
        /// Gets if the character is actually a non-ascii character.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNonAscii(this Char c)
        {
            return c >= 0x80;
        }

        /// <summary>
        /// Gets if the character is actually a non-printable (special)
        /// character.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNonPrintable(this Char c)
        {
            return (c >= 0x0 && c <= 0x8) || (c >= 0xe && c <= 0x1f) || (c >= 0x7f && c <= 0x9f);
        }

        /// <summary>
        /// Gets if the character is actually a (A-Z,a-z) letter.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLetter(this Char c)
        {
            return IsUppercaseAscii(c) || IsLowercaseAscii(c);
        }

        /// <summary>
        /// Gets if the character is actually a name character.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsName(this Char c)
        {
            return c >= 0x80 || c.IsLetter() || c == Symbols.Underscore || c == Symbols.Minus || c.IsDigit();
        }

        /// <summary>
        /// Determines if the given character is a valid character for starting
        /// an identifier.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNameStart(this Char c)
        {
            return c >= 0x80 || IsUppercaseAscii(c) || IsLowercaseAscii(c) || c == Symbols.Underscore;
        }

        /// <summary>
        /// Determines if the given character is a line break character as
        /// specified here:
        /// http://www.w3.org/TR/html401/struct/text.html#h-9.3.2
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsLineBreak(this Char c)
        {
            return c == Symbols.LineFeed || c == Symbols.CarriageReturn;
        }

        /// <summary>
        /// Determines if the given character is a space character as specified
        /// here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#space-character
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSpaceCharacter(this Char c)
        {
            return c == Symbols.Space || c == Symbols.Tab || c == Symbols.LineFeed || c == Symbols.CarriageReturn || c == Symbols.FormFeed;
        }

        /// <summary>
        /// Determines if the given character is a white-space character as
        /// specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#white_space
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWhiteSpaceCharacter(this Char c)
        {
            return c.IsInRange(0x0009, 0x000d) || c == 0x0020 || c == 0x0085 || c == 0x00a0 ||
                    c == 0x1680 || c == 0x180e || c.IsInRange(0x2000, 0x200a) || c == 0x2028 ||
                    c == 0x2029 || c == 0x202f || c == 0x205f || c == 0x3000;
        }

        /// <summary>
        /// Determines if the given character is a digit (0-9) as specified
        /// here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#ascii-digits
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDigit(this Char c)
        {
            return c >= 0x30 && c <= 0x39;
        }

        /// <summary>
        /// Determines if the given character is a valid url code point as specified here:
        /// http://url.spec.whatwg.org/#url-code-points
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        public static Boolean IsUrlCodePoint(this Char c)
        {
            return c.IsAlphanumericAscii() || c == Symbols.ExclamationMark || c == Symbols.Dollar || c == Symbols.Ampersand ||
                   c == Symbols.SingleQuote || c == Symbols.RoundBracketOpen || c == Symbols.RoundBracketClose ||
                   c == Symbols.Asterisk || c == Symbols.Plus || c == Symbols.Minus || c == Symbols.Comma ||
                   c == Symbols.Dot || c == Symbols.Solidus || c == Symbols.Colon || c == Symbols.Semicolon ||
                   c == Symbols.Equality || c == Symbols.QuestionMark || c == Symbols.At || c == Symbols.Underscore ||
                   c == Symbols.Tilde || c.IsInRange(0xa0, 0xd7ff) || c.IsInRange(0xe000, 0xfdcf) || c.IsInRange(0xfdf0, 0xfffd) ||
                   c.IsInRange(0x10000, 0x1FFFD) || c.IsInRange(0x20000, 0x2fffd) || c.IsInRange(0x30000, 0x3fffd) || c.IsInRange(0x40000, 0x4fffd) ||
                   c.IsInRange(0x50000, 0x5fffd) || c.IsInRange(0x60000, 0x6fffd) || c.IsInRange(0x70000, 0x7fffd) || c.IsInRange(0x80000, 0x8fffd) ||
                   c.IsInRange(0x90000, 0x9fffd) || c.IsInRange(0xa0000, 0xafffd) || c.IsInRange(0xb0000, 0xbfffd) || c.IsInRange(0xc0000, 0xcfffd) ||
                   c.IsInRange(0xd0000, 0xdfffd) || c.IsInRange(0xe0000, 0xefffd) || c.IsInRange(0xf0000, 0xffffd) || c.IsInRange(0x100000, 0x10fffd);
        }
    }
}
