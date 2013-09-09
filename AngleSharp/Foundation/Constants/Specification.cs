using System;
using System.Diagnostics;

namespace AngleSharp
{
    /// <summary>
    /// Contains useful information from the specification.
    /// </summary>
    static class Specification
    {
        /// <summary>
        /// The end of file character 26.
        /// </summary>
        public const Char EOF = (Char)0x1a;

        /// <summary>
        /// The tilde character ( ~ ).
        /// </summary>
        public const Char TILDE = (Char)0x7e;

        /// <summary>
        /// The pipe character ( | ).
        /// </summary>
        public const Char PIPE = (Char)0x7c;

        /// <summary>
        /// The null character.
        /// </summary>
        public const Char NULL = (Char)0x0;

        /// <summary>
        /// The ampersand character ( &amp; ).
        /// </summary>
        public const Char AMPERSAND = (Char)0x26;

        /// <summary>
        /// The number sign character ( # ).
        /// </summary>
        public const Char NUM = (Char)0x23;

        /// <summary>
        /// The dollar sign character ( $ ).
        /// </summary>
        public const Char DOLLAR = (Char)0x24;

        /// <summary>
        /// The semicolon sign ( ; ).
        /// </summary>
        public const Char SC = (Char)0x3b;

        /// <summary>
        /// The asterisk character ( * ).
        /// </summary>
        public const Char ASTERISK = (Char)0x2a;

        /// <summary>
        /// The equals sign ( = ).
        /// </summary>
        public const Char EQ = (Char)0x3d;

        /// <summary>
        /// The plus sign ( + ).
        /// </summary>
        public const Char PLUS = (Char)0x2b;

        /// <summary>
        /// The comma character ( , ).
        /// </summary>
        public const Char COMMA = (Char)0x2c;

        /// <summary>
        /// The full stop ( . ).
        /// </summary>
        public const Char DOT = (Char)0x2e;

        /// <summary>
        /// The circumflex accent ( ^ ) character.
        /// </summary>
        public const Char ACCENT = (Char)0x5e;

        /// <summary>
        /// The commercial at ( @ ) character.
        /// </summary>
        public const Char AT = (Char)0x40;

        /// <summary>
        /// The opening angle bracket ( LESS-THAN-SIGN ).
        /// </summary>
        public const Char LT = (Char)0x3c;

        /// <summary>
        /// The closing angle bracket ( GREATER-THAN-SIGN ).
        /// </summary>
        public const Char GT = (Char)0x3e;

        /// <summary>
        /// The single quote / quotation mark ( ' ).
        /// </summary>
        public const Char SQ = (Char)0x27;

        /// <summary>
        /// The (double) quotation mark ( " ).
        /// </summary>
        public const Char DQ = (Char)0x22;

        /// <summary>
        /// The (curved) quotation mark ( ` ).
        /// </summary>
        public const Char CQ = (Char)0x60;

        /// <summary>
        /// The question mark ( ? ).
        /// </summary>
        public const Char QM = (Char)0x3f;

        /// <summary>
        /// The tab character.
        /// </summary>
        public const Char TAB = (Char)0x09;

        /// <summary>
        /// The line feed character.
        /// </summary>
        public const Char LF = (Char)0x0a;

        /// <summary>
        /// The carriage return character.
        /// </summary>
        public const Char CR = (Char)0x0d;

        /// <summary>
        /// The form feed character.
        /// </summary>
        public const Char FF = (Char)0x0c;

        /// <summary>
        /// The space character.
        /// </summary>
        public const Char SPACE = (Char)0x20;

        /// <summary>
        /// The slash (solidus, /) character.
        /// </summary>
        public const Char SOLIDUS = (Char)0x2f;

        /// <summary>
        /// The backslash ( reverse-solidus, \ ) character.
        /// </summary>
        public const Char RSOLIDUS = (Char)0x5c;

        /// <summary>
        /// The colon ( : ) character.
        /// </summary>
        public const Char COLON = (Char)0x3a;

        /// <summary>
        /// The exlamation mark ( ! ) character.
        /// </summary>
        public const Char EM = (Char)0x21;

        /// <summary>
        /// The dash ( hypen minus, - ) character.
        /// </summary>
        public const Char MINUS = (Char)0x2d;

        /// <summary>
        /// The replacement character in case of errors.
        /// </summary>
        public const Char REPLACEMENT = (Char)0xfffd;

        /// <summary>
        /// The low line ( _ ) character.
        /// </summary>
        public const Char UNDERSCORE = (Char)0x5f;

        /// <summary>
        /// The round bracket open ( ( ) character.
        /// </summary>
        public const Char RBO = (Char)0x28;

        /// <summary>
        /// The round bracket close ( ) ) character.
        /// </summary>
        public const Char RBC = (Char)0x29;

        /// <summary>
        /// The square bracket open ( [ ) character.
        /// </summary>
        public const Char SBO = (Char)0x5b;

        /// <summary>
        /// The square bracket close ( ] ) character.
        /// </summary>
		public const Char SBC = (Char)0x5d;

		/// <summary>
		/// The curly bracket open ( { ) character.
		/// </summary>
		public const Char CBO = (Char)0x7b;

		/// <summary>
		/// The curly bracket close ( } ) character.
		/// </summary>
		public const Char CBC = (Char)0x7d;

        /// <summary>
        /// The percent ( % ) character.
        /// </summary>
        public const Char PERCENT = (Char)0x25;

        /// <summary>
        /// The maximum allowed codepoint (defined in Unicode).
        /// </summary>
        public const Int32 MAXIMUM_CODEPOINT = 0x10FFFF;
    }
}
