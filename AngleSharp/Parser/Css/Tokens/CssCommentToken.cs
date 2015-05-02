namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The comment token that contains an HTML comment.
    /// </summary>
    sealed class CssCommentToken : CssToken
    {
        #region ctor

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        public CssCommentToken(CssTokenType type, String data, TextPosition position)
            : base(type, data, position)
        {
        }

        #endregion
    }
}
