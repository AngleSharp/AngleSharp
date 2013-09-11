using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class ElementAnyDeclarationEntry : ElementDeclarationEntry
    {
        public ElementAnyDeclarationEntry()
        {
            _type = ElementContentType.Any;
        }

        public override Boolean Check(Element element)
        {
            return true;
        }
    }
}
