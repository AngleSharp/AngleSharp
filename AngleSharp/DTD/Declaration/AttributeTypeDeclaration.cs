using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    abstract class AttributeTypeDeclaration
    {
    }

    sealed class AttributeStringType : AttributeTypeDeclaration
    {
    }

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
    }

    sealed class AttributeEnumeratedType : AttributeTypeDeclaration
    {
        List<String> _names;

        public AttributeEnumeratedType()
        {
            _names = new List<String>();
        }

        public Boolean IsNotation
        {
            get;
            set;
        }

        public List<String> Names
        {
            get { return _names; }
        }
    }
}
