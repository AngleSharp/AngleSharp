using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class AttributeDeclarationEntry
    {
        public String Name
        {
            get;
            set;
        }

        public AttributeTypeDeclaration ValueType
        {
            get;
            set;
        }

        public Boolean Check(Element element)
        {
            return ValueType.Check(element);
        }
    }
}
