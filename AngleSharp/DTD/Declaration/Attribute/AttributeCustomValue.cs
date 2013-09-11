using System;

namespace AngleSharp.DTD
{
    sealed class AttributeCustomValue : AttributeValueDeclaration
    {
        public Boolean IsFixed
        {
            get;
            set;
        }

        public String Value
        {
            get;
            set;
        }
    }
}
