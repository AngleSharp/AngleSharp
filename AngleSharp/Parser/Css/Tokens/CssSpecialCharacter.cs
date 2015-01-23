namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The special character token that contains a special character such as a colon.
    /// </summary>
    sealed class CssSpecialCharacter : CssCharacterToken
    {
        #region Static instances

        static readonly CssSpecialCharacter colon;
        static readonly CssSpecialCharacter comma;
        static readonly CssSpecialCharacter semicolon;
        static readonly CssSpecialCharacter whitespace;

        #endregion

        #region ctor

        static CssSpecialCharacter()
        {
            colon = new CssSpecialCharacter(Symbols.Colon, CssTokenType.Colon);
            comma = new CssSpecialCharacter(Symbols.Comma, CssTokenType.Comma);
            semicolon = new CssSpecialCharacter(Symbols.Semicolon, CssTokenType.Semicolon);
            whitespace = new CssSpecialCharacter(Symbols.Space, CssTokenType.Whitespace);
        }

        /// <summary>
        /// Creates a new special character token.
        /// </summary>
        /// <param name="c">The character to contain.</param>
        /// <param name="type">The actual token type.</param>
        CssSpecialCharacter(Char c, CssTokenType type)
            : base(type, c)
        {
        }

        #endregion

        #region Properties

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

        #endregion
    }
}
