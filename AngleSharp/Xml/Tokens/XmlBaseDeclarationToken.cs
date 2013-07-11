using System;

namespace AngleSharp.Xml
{
    abstract class XmlBaseDeclarationToken : XmlToken
    {
        #region Members

        String _name;

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
