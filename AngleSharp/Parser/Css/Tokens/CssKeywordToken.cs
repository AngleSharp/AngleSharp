namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// Represents a CSS keyword token.
    /// </summary>
    sealed class CssKeywordToken : CssToken
    {
        #region ctor

        /// <summary>
        /// Creates a new CSS keyword token.
        /// </summary>
        /// <param name="type">The exact type.</param>
        /// <param name="data">The data to use.</param>
        /// <param name="position">The token's position.</param>
        public CssKeywordToken(CssTokenType type, String data, TextPosition position)
            : base(type, data, position)
        {
        }

        #endregion

        #region String representation

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            switch (Type)
            {
                case CssTokenType.Hash:
                    return "#" + Data;
                case CssTokenType.AtKeyword:
                    return "@" + Data;
                default:
                    return Data;
            }
        }

        #endregion
    }
}
