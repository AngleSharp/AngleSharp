using System;

namespace AngleSharp.Html
{
    /// <summary>
    /// The token that is used for comments.
    /// </summary>
    sealed class HtmlCommentToken : HtmlToken
    {
        #region Members

        String _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new comment token.
        /// </summary>
        public HtmlCommentToken()
        {
            _data = string.Empty;
            _type = HtmlTokenType.Comment;
        }

        /// <summary>
        /// Creates a new comment token with the supplied data.
        /// </summary>
        /// <param name="data">The data to set.</param>
        public HtmlCommentToken(String data)
        {
            _type = HtmlTokenType.Comment;
            _data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data of the comment token.
        /// </summary>
        public String Data
        {
            get { return _data; }
            set { _data = value; }
        }

        #endregion
    }
}
