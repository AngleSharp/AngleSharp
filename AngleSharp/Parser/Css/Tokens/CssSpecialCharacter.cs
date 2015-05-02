namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The special character token that contains a special character such as a colon.
    /// </summary>
    sealed class CssSpecialCharacter : CssCharacterToken
    {
        #region ctor

        /// <summary>
        /// Creates a new special character token.
        /// </summary>
        /// <param name="c">The character to contain.</param>
        /// <param name="type">The actual token type.</param>
        /// <param name="position">The token's position.</param>
        CssSpecialCharacter(Char c, CssTokenType type, TextPosition position)
            : base(type, c, position)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Creates a colon token.
        /// </summary>
        public static CssSpecialCharacter Colon(TextPosition position)
        {
            return new CssSpecialCharacter(Symbols.Colon, CssTokenType.Colon, position);
        }

        /// <summary>
        /// Creates a new comma token.
        /// </summary>
        public static CssSpecialCharacter Comma(TextPosition position)
        {
            return new CssSpecialCharacter(Symbols.Comma, CssTokenType.Comma, position);
        }

        /// <summary>
        /// Creates a new comma token.
        /// </summary>
        public static CssSpecialCharacter Semicolon(TextPosition position)
        {
            return new CssSpecialCharacter(Symbols.Semicolon, CssTokenType.Semicolon, position);
        }

        /// <summary>
        /// Creates a new comma token.
        /// </summary>
        public static CssSpecialCharacter Whitespace(TextPosition position)
        {
            return new CssSpecialCharacter(Symbols.Space, CssTokenType.Whitespace, position);
        }

        #endregion
    }
}
