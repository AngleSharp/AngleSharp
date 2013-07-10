using System;

namespace AngleSharp.Xml
{
    sealed class XmlAttributeDeclarationToken : XmlToken
    {
        #region Members

        String _name;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlAttributeDeclarationToken()
        {
            _type = XmlTokenType.AttributeDeclaration;
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        #endregion
    }
}
