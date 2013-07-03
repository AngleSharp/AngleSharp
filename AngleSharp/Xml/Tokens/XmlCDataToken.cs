using System;

namespace AngleSharp.Xml
{
    /// <summary>
    /// The CData token that contains a sequence of raw characters.
    /// </summary>
    sealed class XmlCDataToken : XmlToken
    {
        #region Members

        String _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CData token.
        /// </summary>
        public XmlCDataToken()
        {
            _data = String.Empty;
            _type = XmlTokenType.CData;
        }

        /// <summary>
        /// Creates a new CData token with the supplied data.
        /// </summary>
        /// <param name="data">The data to set.</param>
        public XmlCDataToken(String data)
        {
            _type = XmlTokenType.CData;
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
