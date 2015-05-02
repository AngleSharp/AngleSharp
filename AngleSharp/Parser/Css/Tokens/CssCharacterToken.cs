namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The character token that contains a series of characters.
    /// </summary>
    abstract class CssCharacterToken : CssToken
    {
        #region ctor

        /// <summary>
        /// Creates a new character token with the given character.
        /// </summary>
        /// <param name="type">The type of token.</param>
        /// <param name="data">The character.</param>
        /// <param name="position">The token's position.</param>
        public CssCharacterToken(CssTokenType type, Char data, TextPosition position)
            : base(type, data.ToString(), position)
        {
        }

        #endregion
    }
}
