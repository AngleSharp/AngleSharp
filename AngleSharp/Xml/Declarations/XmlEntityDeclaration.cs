using System;

namespace AngleSharp.Xml
{
    sealed class XmlEntityDeclaration : XmlBaseDeclaration
    {
        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlEntityDeclaration()
        {
        }

        #endregion

        #region Properties

        public Boolean IsParameter 
        { 
            get; 
            set;
        }

        public Boolean IsExtern
        {
            get;
            set;
        }

        public String Value
        {
            get;
            set;
        }

        public String ExternNotation
        {
            get;
            set;
        }

        #endregion
    }
}
