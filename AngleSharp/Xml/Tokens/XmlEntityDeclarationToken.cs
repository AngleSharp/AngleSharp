using System;

namespace AngleSharp.Xml
{
    sealed class XmlEntityDeclarationToken : XmlBaseDeclarationToken
    {
        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlEntityDeclarationToken()
        {
            _type = XmlTokenType.EntityDeclaration;
        }

        #endregion
    }
}
