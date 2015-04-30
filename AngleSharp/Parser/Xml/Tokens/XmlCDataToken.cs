namespace AngleSharp.Parser.Xml
{
    using System;

    /// <summary>
    /// The CData token that contains a sequence of raw characters.
    /// </summary>
    sealed class XmlCDataToken : XmlToken
    {
        #region Fields

        readonly String _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CData token.
        /// </summary>
        public XmlCDataToken(TextPosition position)
            : this(position, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new CData token with the supplied data.
        /// </summary>
        public XmlCDataToken(TextPosition position, String data)
            : base(XmlTokenType.CData, position)
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
