using System;

namespace AngleSharp.DTD
{
    abstract class AttributeValueDeclaration
    {
    }

    sealed class AttributeRequiredValue : AttributeValueDeclaration
    {

    }

    sealed class AttributeImpliedValue : AttributeValueDeclaration
    {

    }

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
