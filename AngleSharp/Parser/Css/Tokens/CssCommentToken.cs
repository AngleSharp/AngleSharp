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
        CssCommentToken(CssTokenType type, String data, TextPosition position)
            : base(type, data, position)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Creates a new CSS open comment token.
        /// </summary>
        public static CssCommentToken Open(TextPosition position)
        {
            return new CssCommentToken(CssTokenType.Cdo, "<!--", position);
        }

        /// <summary>
        /// Creates a new CSS close comment token.
        /// </summary>
        public static CssCommentToken Close(TextPosition position)
        {
            return new CssCommentToken(CssTokenType.Cdc, "-->", position);
        }

        #endregion
    }
}
