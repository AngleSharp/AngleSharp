namespace AngleSharp.Xml.Parser.Tokens
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the XML declaration &lt;?xml ...?&gt;
    /// </summary>
    sealed class XmlDeclarationToken : XmlToken
    {
        #region Fields

        private String _version;
        private String _encoding;
        private Boolean _standalone;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new XML declaration token.
        /// </summary>
        public XmlDeclarationToken(TextPosition position)
            : base(XmlTokenType.Declaration, position)
        {
            _version = String.Empty;
            _encoding = null;
            _standalone = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the encoding value has been set.
        /// </summary>
        public Boolean IsEncodingMissing => _encoding == null;

        /// <summary>
        /// Gets or sets the version value.
        /// </summary>
        public String Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// Gets or sets the encoding value.
        /// </summary>
        public String Encoding
        {
            get { return _encoding ?? String.Empty; }
            set { _encoding = value; }
        }

        /// <summary>
        /// Gets or sets the standalone value.
        /// </summary>
        public Boolean Standalone
        {
            get { return _standalone; }
            set { _standalone = value; }
        }

        #endregion
    }
}
