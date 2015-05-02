namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The match token that contains part of a selector.
    /// </summary>
    sealed class CssMatchToken : CssToken
    {
        #region ctor

        /// <summary>
        /// Creates a new CSS match token.
        /// </summary>
        public CssMatchToken(CssTokenType type, String data, TextPosition position)
            : base(type, data, position)
        {
        }

        #endregion
    }
}
