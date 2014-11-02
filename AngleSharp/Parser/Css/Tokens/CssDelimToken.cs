namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The delimiter token that contains a series of characters.
    /// </summary>
    sealed class CssDelimToken : CssCharacterToken
    {
        #region ctor

        /// <summary>
        /// Creates a new delimiter token with the given character.
        /// </summary>
        /// <param name="data">The character.</param>
        public CssDelimToken(Char data)
            : base(CssTokenType.Delim, data)
        {
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            return Data;
        }

        #endregion
    }
}
