namespace AngleSharp.Parser.Css
{
    /// <summary>
    /// The column token that contains a column (||).
    /// </summary>
    sealed class CssColumnToken : CssToken
    {
        #region ctor

        /// <summary>
        /// Creates a new CSS column token.
        /// </summary>
        public CssColumnToken(TextPosition position)
            : base(CssTokenType.Column, "||", position)
        {
        }

        #endregion
    }
}
