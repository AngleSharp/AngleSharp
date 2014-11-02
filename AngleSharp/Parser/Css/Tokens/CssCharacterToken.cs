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
        /// <param name="data">The character.</param>
        public CssCharacterToken(CssTokenType type, Char data)
            : base(type, data.ToString())
        {
        }

        #endregion
    }
}
