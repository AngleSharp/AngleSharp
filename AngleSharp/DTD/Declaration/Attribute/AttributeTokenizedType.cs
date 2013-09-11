using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class AttributeTokenizedType : AttributeTypeDeclaration
    {
        public TokenizedType Value
        {
            get;
            set;
        }

        #region Enumeration

        public enum TokenizedType
        {
            ID,
            IDREF,
            IDREFS,
            ENTITY,
            ENTITIES,
            NMTOKEN,
            NMTOKENS
        }

        #endregion

        public override Boolean Check(Element element)
        {
            return true;
        }
    }
}
