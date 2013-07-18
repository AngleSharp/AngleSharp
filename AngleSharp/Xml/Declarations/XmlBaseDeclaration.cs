using System;

namespace AngleSharp.Xml
{
    abstract class XmlBaseDeclaration : XmlToken
    {
        #region Members

        String _name;

        #endregion

        #region ctor

        public XmlBaseDeclaration()
        {
            _type = XmlTokenType.DeclarationInstruction;
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
