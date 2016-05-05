using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class AttributeStringType : AttributeTypeDeclaration
    {
        public override Boolean Check(Element element)
        {
            return true;
        }
    }
}
