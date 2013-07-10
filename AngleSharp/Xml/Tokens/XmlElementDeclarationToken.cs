using System;

namespace AngleSharp.Xml
{
    sealed class XmlElementDeclarationToken : XmlToken
    {
        #region Members

        String _name;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlElementDeclarationToken()
        {
            _type = XmlTokenType.ElementDeclaration;
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
