namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The bracket token that contains the opening or closing of a bracket.
    /// </summary>
    sealed class CssBracketToken : CssToken
    {
        #region ctor

        /// <summary>
        /// Creates a new CSS bracket token.
        /// </summary>
        public CssBracketToken(CssTokenType type, String bracket, TextPosition position)
            : base(type, bracket, position)
        {
        }

        #endregion
    }
}
