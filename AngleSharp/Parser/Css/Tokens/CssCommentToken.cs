namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The comment token that contains an HTML comment.
    /// </summary>
    sealed class CssCommentToken : CssToken
    {
        #region Static instances

        readonly static CssCommentToken open;
        readonly static CssCommentToken close;

        #endregion

        #region ctor

        static CssCommentToken()
        {
            open = new CssCommentToken(CssTokenType.Cdo, "<!--");
            close = new CssCommentToken(CssTokenType.Cdc, "-->");
        }

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        CssCommentToken(CssTokenType type, String data)
            : base(type, data)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a new CSS open comment token.
        /// </summary>
        public static CssCommentToken Open
        {
            get { return open; }
        }

        /// <summary>
        /// Gets a new CSS close comment token.
        /// </summary>
        public static CssCommentToken Close
        {
            get { return close; }
        }

        #endregion
    }
}
