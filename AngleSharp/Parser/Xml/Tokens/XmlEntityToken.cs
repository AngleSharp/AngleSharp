namespace AngleSharp.Parser.Xml
{
    using System;

    /// <summary>
    /// The entity token that defines an entity.
    /// </summary>
    sealed class XmlEntityToken : XmlToken
    {
        #region Fields

        String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlEntityToken()
        {
            _type = XmlTokenType.Entity;
        }

        #endregion

        #region Properties

        public Boolean IsNumeric
        {
            get;
            set;
        }

        public Boolean IsHex
        {
            get;
            set;
        }

        public String Value
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion
    }
}
