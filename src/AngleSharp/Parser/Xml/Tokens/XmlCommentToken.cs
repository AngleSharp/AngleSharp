namespace AngleSharp.Parser.Xml
{
    using System;

    /// <summary>
    /// The token that is used for comments.
    /// </summary>
    sealed class XmlCommentToken : XmlToken
    {
        #region Fields

        readonly String _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new comment token.
        /// </summary>
        public XmlCommentToken(TextPosition position)
            : this(position, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new comment token with the supplied data.
        /// </summary>
        public XmlCommentToken(TextPosition position, String data)
            : base(XmlTokenType.Comment, position)
        {
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
        }

        #endregion
    }
}
