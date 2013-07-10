using System;

namespace AngleSharp.Xml
{
    sealed class XmlEntityDeclarationToken : XmlToken
    {
        #region Members

        String _name;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlEntityDeclarationToken()
        {
            _type = XmlTokenType.EntityDeclaration;
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
