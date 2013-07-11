using System;

namespace AngleSharp.Xml
{
    sealed class XmlAttributeDeclarationToken : XmlBaseDeclarationToken
    {
        #region ctor

        /// <summary>
        /// Creates a new entity token.
        /// </summary>
        public XmlAttributeDeclarationToken()
        {
            _type = XmlTokenType.AttributeDeclaration;
        }

        #endregion
    }
}
