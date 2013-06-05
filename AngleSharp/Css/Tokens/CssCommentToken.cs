using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// The comment token that contains an HTML comment.
    /// </summary>
    class CssCommentToken : CssToken
    {
        readonly static CssCommentToken open = new CssCommentToken { _type = CssTokenType.Cdo };
        readonly static CssCommentToken close = new CssCommentToken { _type = CssTokenType.Cdc };

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        private CssCommentToken()
        {
        }

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

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override string ToValue()
        {
            return _type == CssTokenType.Cdo ? "<!--" : "-->";
        }
    }
}
