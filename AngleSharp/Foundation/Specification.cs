using System;
using System.Diagnostics;

namespace AngleSharp
{
    /// <summary>
    /// Contains useful information from the specification.
    /// </summary>
    static class Specification
    {
        #region Constants

        /// <summary>
        /// Gets the XML annotation string annotation-xml
        /// </summary>
        public const string XML_ANNOTATION = "annotation-xml";

        /// <summary>
        /// The end of file character 26.
        /// </summary>
        public const char EOF = (char)0x1a;

        /// <summary>
        /// The tilde character (~).
        /// </summary>
        public const char TILDE = (char)0x7e;

        /// <summary>
        /// The tilde character (~).
        /// </summary>
        public const char PIPE = (char)0x7c;

        /// <summary>
        /// The null character.
        /// </summary>
        public const char NULL = (char)0x0;

        /// <summary>
        /// The ampersand character (&amp;).
        /// </summary>
        public const char AMPERSAND = (char)0x26;

        /// <summary>
        /// The number sign character (#).
        /// </summary>
        public const char NUM = (char)0x23;

        /// <summary>
        /// The dollar sign character ($).
        /// </summary>
        public const char DOLLAR = (char)0x24;

        /// <summary>
        /// The semicolon sign (;).
        /// </summary>
        public const char SC = (char)0x3b;

        /// <summary>
        /// The asterisk character (*).
        /// </summary>
        public const char ASTERISK = (char)0x2a;

        /// <summary>
        /// The equals sign (=).
        /// </summary>
        public const char EQ = (char)0x3d;

        /// <summary>
        /// The plus sign (+).
        /// </summary>
        public const char PLUS = (char)0x2b;

        /// <summary>
        /// The comma character (,).
        /// </summary>
        public const char COMMA = (char)0x2c;

        /// <summary>
        /// The full stop (.).
        /// </summary>
        public const char FS = (char)0x2e;

        /// <summary>
        /// The circumflex accent (^) character.
        /// </summary>
        public const char CA = (char)0x5e;

        /// <summary>
        /// The commercial at (@) character.
        /// </summary>
        public const char AT = (char)0x40;

        /// <summary>
        /// The opening angle bracket (LESS-THAN-SIGN).
        /// </summary>
        public const char LT = (char)0x3c;

        /// <summary>
        /// The closing angle bracket (GREATER-THAN-SIGN).
        /// </summary>
        public const char GT = (char)0x3e;

        /// <summary>
        /// The single quote / quotation mark (').
        /// </summary>
        public const char SQ = (char)0x27;

        /// <summary>
        /// The (double) quotation mark (").
        /// </summary>
        public const char DQ = (char)0x22;

        /// <summary>
        /// The (curved) quotation mark (`).
        /// </summary>
        public const char CQ = (char)0x60;

        /// <summary>
        /// The question mark (?).
        /// </summary>
        public const char QM = (char)0x3f;

        /// <summary>
        /// The tab character.
        /// </summary>
        public const char TAB = (char)0x09;

        /// <summary>
        /// The line feed character.
        /// </summary>
        public const char LF = (char)0x0a;

        /// <summary>
        /// The carriage return character.
        /// </summary>
        public const char CR = (char)0x0d;

        /// <summary>
        /// The form feed character.
        /// </summary>
        public const char FF = (char)0x0c;

        /// <summary>
        /// The space character.
        /// </summary>
        public const char SPACE = (char)0x20;

        /// <summary>
        /// The slash (solidus, /) character.
        /// </summary>
        public const char SOLIDUS = (char)0x2f;

        /// <summary>
        /// The backslash (reverse-solidus, \) character.
        /// </summary>
        public const char RSOLIDUS = (char)0x5c;

        /// <summary>
        /// The colon (:) character.
        /// </summary>
        public const char COL = (char)0x3a;

        /// <summary>
        /// The exlamation mark (!) character.
        /// </summary>
        public const char EM = (char)0x21;

        /// <summary>
        /// The dash (hypen minus, -) character.
        /// </summary>
        public const char DASH = (char)0x2d;

        /// <summary>
        /// The replacement character in case of errors.
        /// </summary>
        public const char REPLACEMENT = (char)0xfffd;

        /// <summary>
        /// The low line (_) character.
        /// </summary>
        public const char LL = (char)0x5f;

        /// <summary>
        /// The maximum allowed codepoint (defined in Unicode).
        /// </summary>
        public const int MAXIMUM_CODEPOINT = 0x10FFFF;

        #endregion

        #region Methods

        /// <summary>
        /// Gets if the character is actually a non-ascii character.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsNonAscii(Char c)
        {
            return c >= 0x80;
        }

        /// <summary>
        /// Gets if the character is actually a non-printable (special) character.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsNonPrintable(Char c)
        {
            return (c >= 0x0 && c <= 0x8) || (c >= 0xe && c <= 0x1f) || (c >= 0x7f && c <= 0x9f);
        }

        /// <summary>
        /// Gets if the character is actually a (A-Z,a-z) letter.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsLetter(Char c)
        {
            return IsUppercaseAscii(c) || IsLowercaseAscii(c);
        }

        /// <summary>
        /// Gets if the character is actually a name character.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsName(Char c)
        {
            return c >= 0x80 || IsLetter(c) || c == LL || c == DASH || IsDigit(c);
        }

        /// <summary>
        /// Determines if the given character is a valid character for starting an identifier.
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsNameStart(Char c)
        {
            return c >= 0x80 || IsUppercaseAscii(c) || IsLowercaseAscii(c) || c == LL;
        }

        /// <summary>
        /// Determines if the given character is a line break character as specified here:
        /// http://www.w3.org/TR/html401/struct/text.html#h-9.3.2
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsLineBreak(Char c)
        {
            //line feed, carriage return
            return c == LF || c == CR;
        }

        /// <summary>
        /// Determines if the given character is a space character as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#space-character
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsSpaceCharacter(Char c)
        {
            //white space, tab, line feed, form feed, carriage return
            return c == SPACE || c == TAB || c == LF || c == FF || c == CR;
        }

        /// <summary>
        /// Determines if the given character is a white-space character as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#white_space
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsWhiteSpaceCharacter(Char c)
        {
            return (c >= 0x0009 && c <= 0x000d) || c == 0x0020 || c == 0x0085 || c == 0x00a0 ||
                    c == 0x1680 || c == 0x180e || (c >= 0x2000 && c <= 0x200a) || c == 0x2028 ||
                    c == 0x2029 || c == 0x202f || c == 0x205f || c == 0x3000;
        }

        /// <summary>
        /// Determines if the given character is a digit (0-9) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#ascii-digits
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsDigit(Char c)
        {
            return c >= 0x30 && c <= 0x39;
        }

        /// <summary>
        /// Determines if the given string consists only of digits (0-9) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#ascii-digits
        /// </summary>
        /// <param name="s">The characters to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsDigit(String s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!IsDigit(s[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines if the given character is a uppercase character (A-Z) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#uppercase-ascii-letters
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsUppercaseAscii(Char c)
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
        public static Boolean IsLowercaseAscii(Char c)
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
        public static Boolean IsAlphanumericAscii(Char c)
        {
            return IsDigit(c) || IsUppercaseAscii(c) || IsLowercaseAscii(c);
        }

        /// <summary>
        /// Determines if the given character is a hexadecimal (0-9a-fA-F) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#ascii-hex-digits
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsHex(Char c)
        {
            return IsDigit(c) || (c >= 0x41 && c <= 0x46) || (c >= 0x61 && c <= 0x66);
        }

        /// <summary>
        /// Determines if the given string only contains characters, which are hexadecimal (0-9a-fA-F) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#ascii-hex-digits
        /// </summary>
        /// <param name="s">The string to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsHex(String s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!IsHex(s[i]))
                    return false;
            }

            return true;
        }

        #endregion
    }
}
