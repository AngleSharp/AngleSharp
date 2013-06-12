using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// The special character token that contains a special character such as a colon.
    /// </summary>
    sealed class CssSpecialCharacter : CssCharacterToken
    {
        static readonly CssSpecialCharacter colon = new CssSpecialCharacter(Specification.COL, CssTokenType.Colon);
        static readonly CssSpecialCharacter comma = new CssSpecialCharacter(Specification.COMMA, CssTokenType.Comma);
        static readonly CssSpecialCharacter semicolon = new CssSpecialCharacter(Specification.SC, CssTokenType.Semicolon);
        static readonly CssSpecialCharacter whitespace = new CssSpecialCharacter(Specification.SPACE, CssTokenType.Whitespace);

        /// <summary>
        /// Creates a new special character token.
        /// </summary>
        /// <param name="c">The character to contain.</param>
        /// <param name="type">The actual token type.</param>
        private CssSpecialCharacter(char c, CssTokenType type)
            : base(c)
        {
            _type = type;
        }

        /// <summary>
        /// Gets a colon token.
        /// </summary>
        public static CssSpecialCharacter Colon
        {
            get { return colon; }
        }

        /// <summary>
        /// Gets a new comma token.
        /// </summary>
        public static CssSpecialCharacter Comma
        {
            get { return comma; }
        }

        /// <summary>
        /// Gets a new comma token.
        /// </summary>
        public static CssSpecialCharacter Semicolon
        {
            get { return semicolon; }
        }

        /// <summary>
        /// Gets a new comma token.
        /// </summary>
        public static CssSpecialCharacter Whitespace
        {
            get { return whitespace; }
        }

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override string ToValue()
        {
            return Data.ToString();
        }
    }
}
