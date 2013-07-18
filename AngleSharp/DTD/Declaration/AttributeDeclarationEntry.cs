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

        public AttributeValueDeclaration ValueDefault
        {
            get;
            set;
        }
    }
}
