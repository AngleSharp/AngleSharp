using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class AttributeImpliedValue : AttributeValueDeclaration
    {
        public override Boolean Apply(Element element)
        {
            return true;
        }
    }
}
