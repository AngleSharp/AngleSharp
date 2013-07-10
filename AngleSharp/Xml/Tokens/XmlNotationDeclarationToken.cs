using System;

namespace AngleSharp.Xml
{
    sealed class XmlNotationDeclarationToken : XmlToken
    {
        #region Members

        String _name;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlNotationDeclarationToken()
        {
            _type = XmlTokenType.NotationDeclaration;
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
