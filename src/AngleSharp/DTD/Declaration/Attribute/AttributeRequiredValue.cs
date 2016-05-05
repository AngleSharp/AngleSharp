using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class AttributeRequiredValue : AttributeValueDeclaration
    {
        public override Boolean Apply(Element element)
        {
            return element.HasAttribute(Parent.Name);
        }
    }
}
