using AngleSharp.DOM;
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

        public override Boolean Apply(Element element)
        {
            if (IsFixed || !element.HasAttribute(Parent.Name))
                element.SetAttribute(Parent.Name, Value);

            return true;
        }
    }
}
