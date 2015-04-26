namespace AngleSharp
{
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// Contains useful information from the specification.
    /// </summary>
    static class Symbols
    {
        /// <summary>
        /// The end of file character 26.
        /// </summary>
        public const Char EndOfFile = (Char)0x1a;

        /// <summary>
        /// The tilde character ( ~ ).
        /// </summary>
        public const Char Tilde = (Char)0x7e;

        /// <summary>
        /// The pipe character ( | ).
        /// </summary>
        public const Char Pipe = (Char)0x7c;

        /// <summary>
        /// The null character.
        /// </summary>
        public const Char Null = (Char)0x0;

        /// <summary>
        /// The ampersand character ( &amp; ).
        /// </summary>
        public const Char Ampersand = (Char)0x26;

        /// <summary>
        /// The number sign character ( # ).
        /// </summary>
        public const Char Num = (Char)0x23;

        /// <summary>
        /// The dollar sign character ( $ ).
        /// </summary>
        public const Char Dollar = (Char)0x24;

        /// <summary>
        /// The semicolon sign ( ; ).
        /// </summary>
        public const Char Semicolon = (Char)0x3b;

        /// <summary>
        /// The asterisk character ( * ).
        /// </summary>
        public const Char Asterisk = (Char)0x2a;

        /// <summary>
        /// The equals sign ( = ).
        /// </summary>
        public const Char Equality = (Char)0x3d;

        /// <summary>
        /// The plus sign ( + ).
        /// </summary>
        public const Char Plus = (Char)0x2b;

        /// <summary>
        /// The dash ( hypen minus, - ) character.
        /// </summary>
        public const Char Minus = (Char)0x2d;

        /// <summary>
        /// The comma character ( , ).
        /// </summary>
        public const Char Comma = (Char)0x2c;

        /// <summary>
        /// The full stop ( . ).
        /// </summary>
        public const Char Dot = (Char)0x2e;

        /// <summary>
        /// The circumflex accent ( ^ ) character.
        /// </summary>
        public const Char Accent = (Char)0x5e;

        /// <summary>
        /// The commercial at ( @ ) character.
        /// </summary>
        public const Char At = (Char)0x40;

        /// <summary>
        /// The opening angle bracket ( LESS-THAN-SIGN ).
        /// </summary>
        public const Char LessThan = (Char)0x3c;

        /// <summary>
        /// The closing angle bracket ( GREATER-THAN-SIGN ).
        /// </summary>
        public const Char GreaterThan = (Char)0x3e;

        /// <summary>
        /// The single quote / quotation mark ( ' ).
        /// </summary>
        public const Char SingleQuote = (Char)0x27;

        /// <summary>
        /// The (double) quotation mark ( " ).
        /// </summary>
        public const Char DoubleQuote = (Char)0x22;

        /// <summary>
        /// The (curved) quotation mark ( ` ).
        /// </summary>
        public const Char CurvedQuote = (Char)0x60;

        /// <summary>
        /// The question mark ( ? ).
        /// </summary>
        public const Char QuestionMark = (Char)0x3f;

        /// <summary>
        /// The tab character.
        /// </summary>
        public const Char Tab = (Char)0x09;

        /// <summary>
        /// The line feed character.
        /// </summary>
        public const Char LineFeed = (Char)0x0a;

        /// <summary>
        /// The carriage return character.
        /// </summary>
        public const Char CarriageReturn = (Char)0x0d;

        /// <summary>
        /// The form feed character.
        /// </summary>
        public const Char FormFeed = (Char)0x0c;

        /// <summary>
        /// The space character.
        /// </summary>
        public const Char Space = (Char)0x20;

        /// <summary>
        /// The slash (solidus, /) character.
        /// </summary>
        public const Char Solidus = (Char)0x2f;

        /// <summary>
        /// The no break space character.
        /// </summary>
        public const Char NoBreakSpace = (Char)0xa0;

        /// <summary>
        /// The backslash ( reverse-solidus, \ ) character.
        /// </summary>
        public const Char ReverseSolidus = (Char)0x5c;

        /// <summary>
        /// The colon ( : ) character.
        /// </summary>
        public const Char Colon = (Char)0x3a;

        /// <summary>
        /// The exclamation mark ( ! ) character.
        /// </summary>
        public const Char ExclamationMark = (Char)0x21;

        /// <summary>
        /// The replacement character in case of errors.
        /// </summary>
        public const Char Replacement = (Char)0xfffd;

        /// <summary>
        /// The low line ( _ ) character.
        /// </summary>
        public const Char Underscore = (Char)0x5f;

        /// <summary>
        /// The round bracket open ( ( ) character.
        /// </summary>
        public const Char RoundBracketOpen = (Char)0x28;

        /// <summary>
        /// The round bracket close ( ) ) character.
        /// </summary>
        public const Char RoundBracketClose = (Char)0x29;

        /// <summary>
        /// The square bracket open ( [ ) character.
        /// </summary>
        public const Char SquareBracketOpen = (Char)0x5b;

        /// <summary>
        /// The square bracket close ( ] ) character.
        /// </summary>
		public const Char SquareBracketClose = (Char)0x5d;

		/// <summary>
		/// The curly bracket open ( { ) character.
		/// </summary>
		public const Char CurlyBracketOpen = (Char)0x7b;

		/// <summary>
		/// The curly bracket close ( } ) character.
		/// </summary>
		public const Char CurlyBracketClose = (Char)0x7d;

        /// <summary>
        /// The percent ( % ) character.
        /// </summary>
        public const Char Percent = (Char)0x25;

        /// <summary>
        /// The maximum allowed codepoint (defined in Unicode).
        /// </summary>
        public const Int32 MaximumCodepoint = 0x10FFFF;

        /// <summary>
        /// A list of available punycode character mappings.
        /// </summary>
        public static Dictionary<Char, Char> Punycode = new Dictionary<Char, Char>
        {
            { '。', '.' },
            { '．', '.' },
            { 'Ｇ', 'g' },
            { 'ｏ', 'o' },
            { 'ｃ', 'c' },
            { 'Ｘ', 'x' },
            { '０', '0' },
            { '１', '1' },
            { '２', '2' },
            { '５', '5' },
        };
    }
}
