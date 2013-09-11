using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class AttributeTokenizedType : AttributeTypeDeclaration
    {
        #region Properties

        public TokenizedType Value
        {
            get;
            set;
        }

        #endregion

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

        #region Methods

        public override Boolean Check(Element element)
        {
            return true;
        }

        #endregion
    }
}
