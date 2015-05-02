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
        public CssSpecialCharacter(Char c, CssTokenType type, TextPosition position)
            : base(type, c, position)
        {
        }

        #endregion
    }
}
