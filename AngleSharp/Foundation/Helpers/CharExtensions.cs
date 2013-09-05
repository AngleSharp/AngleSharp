using System;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using System.Diagnostics;

namespace AngleSharp
{
    /// <summary>
    /// Useful methods for char objects.
    /// </summary>
    static class CharExtensions
    {
        /// <summary>
        /// Examines if the given element is one of the table elements (table, tbody, tfoot, thead, tr).
        /// </summary>
        /// <param name="node">The node to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        public static Boolean IsTableElement(this Node node)
        {
            return (node is HTMLTableElement || node is HTMLTableSectionElement || node is HTMLTableRowElement);
        }

        /// <summary>
        /// Converts a given character from the hex representation (0-9A-Fa-f) to an integer.
        /// </summary>
        /// <param name="c">The character to convert.</param>
        /// <returns>The integer value or undefined behavior if invalid.</returns>
        [DebuggerStepThrough]
        public static Int32 FromHex(this Char c)
        {
            return c.IsDigit() ? c - 0x30 : c - (c.IsLowercaseAscii() ? 0x57 : 0x37);
        }

        /// <summary>
        /// Transforms the given number to a hexadecimal string.
        /// </summary>
        /// <param name="num">The number (0-255).</param>
        /// <returns>A 2 digit upper case hexadecimal string.</returns>
        [DebuggerStepThrough]
        public static String ToHex(this Byte num)
        {
            Char[] chrs = new Char[2];
            var rem = num >> 4;
            chrs[0] = (Char)(rem + (rem < 10 ? 48 : 55));
            rem = num - 16 * rem;
            chrs[1] = (Char)(rem + (rem < 10 ? 48 : 55));
            return new String(chrs);
        }

        /// <summary>
        /// Determines if the given character is in the given range.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <param name="lower">The lower bound of the range.</param>
        /// <param name="upper">The upper bound of the range.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsInRange(this Char c, Int32 lower, Int32 upper)
        {
            return c >= lower && c <= upper;
        }

        /// <summary>
        /// Determines if the given character is a uppercase character (A-Z) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#uppercase-ascii-letters
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsUppercaseAscii(this Char c)
        {
            return c >= 0x41 && c <= 0x5a;
        }

        /// <summary>
        /// Determines if the given character is a lowercase character (a-z) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#lowercase-ascii-letters
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsLowercaseAscii(this Char c)
        {
            return c >= 0x61 && c <= 0x7a;
        }

        /// <summary>
        /// Determines if the given character is a alphanumeric character (0-9a-zA-z) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#alphanumeric-ascii-characters
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsAlphanumericAscii(this Char c)
        {
            return c.IsDigit() || c.IsUppercaseAscii() || c.IsLowercaseAscii();
        }

        /// <summary>
        /// Determines if the given character is a hexadecimal (0-9a-fA-F) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#ascii-hex-digits
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsHex(this Char c)
        {
            return c.IsDigit() || (c >= 0x41 && c <= 0x46) || (c >= 0x61 && c <= 0x66);
        }

        /// <summary>
        /// Gets if the character is actually a non-ascii character.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsNonAscii(this Char c)
        {
            return c >= 0x80;
        }

        /// <summary>
        /// Gets if the character is actually a non-printable (special) character.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsNonPrintable(this Char c)
        {
            return (c >= 0x0 && c <= 0x8) || (c >= 0xe && c <= 0x1f) || (c >= 0x7f && c <= 0x9f);
        }

        /// <summary>
        /// Gets if the character is actually a (A-Z,a-z) letter.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsLetter(this Char c)
        {
            return IsUppercaseAscii(c) || IsLowercaseAscii(c);
        }

        /// <summary>
        /// Gets if the character is actually a name character.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsName(this Char c)
        {
            return c >= 0x80 || c.IsLetter() || c == Specification.UNDERSCORE || c == Specification.MINUS || IsDigit(c);
        }

        /// <summary>
        /// Determines if the given character is a valid character for starting an identifier.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsNameStart(this Char c)
        {
            return c >= 0x80 || IsUppercaseAscii(c) || IsLowercaseAscii(c) || c == Specification.UNDERSCORE;
        }

        /// <summary>
        /// Determines if the given character is a line break character as specified here:
        /// http://www.w3.org/TR/html401/struct/text.html#h-9.3.2
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsLineBreak(this Char c)
        {
            //line feed, carriage return
            return c == Specification.LF || c == Specification.CR;
        }

        /// <summary>
        /// Determines if the given character is a space character as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#space-character
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsSpaceCharacter(this Char c)
        {
            //white space, tab, line feed, form feed, carriage return
            return c == Specification.SPACE || c == Specification.TAB || c == Specification.LF || c == Specification.FF || c == Specification.CR;
        }

        /// <summary>
        /// Determines if the given character is a white-space character as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#white_space
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsWhiteSpaceCharacter(this Char c)
        {
            return c.IsInRange(0x0009, 0x000d) || c == 0x0020 || c == 0x0085 || c == 0x00a0 ||
                    c == 0x1680 || c == 0x180e || c.IsInRange(0x2000, 0x200a) || c == 0x2028 ||
                    c == 0x2029 || c == 0x202f || c == 0x205f || c == 0x3000;
        }

        /// <summary>
        /// Determines if the given character is a digit (0-9) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#ascii-digits
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsDigit(this Char c)
        {
            return c >= 0x30 && c <= 0x39;
        }
    }
}
