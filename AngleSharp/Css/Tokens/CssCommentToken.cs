using System;

namespace AngleSharp.Css
{
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
            open = new CssCommentToken { _type = CssTokenType.Cdo };
            close = new CssCommentToken { _type = CssTokenType.Cdc };
        }

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        CssCommentToken()
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

        #region Methods

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            return _type == CssTokenType.Cdo ? "<!--" : "-->";
        }

        #endregion
    }
}
