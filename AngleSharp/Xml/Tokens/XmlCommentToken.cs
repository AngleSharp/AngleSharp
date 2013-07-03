using System;

namespace AngleSharp.Xml
{
    /// <summary>
    /// The token that is used for comments.
    /// </summary>
    sealed class XmlCommentToken : XmlToken
    {
        #region Members

        String _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new comment token.
        /// </summary>
        public XmlCommentToken()
        {
            _data = String.Empty;
            _type = XmlTokenType.Comment;
        }

        /// <summary>
        /// Creates a new comment token with the supplied data.
        /// </summary>
        /// <param name="data">The data to set.</param>
        public XmlCommentToken(String data)
        {
            _type = XmlTokenType.Comment;
            _data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the supplied data.
        /// </summary>
        public String Data 
        {
            get { return _data; }
            set { _data = value; }
        }

        #endregion
    }
}
