using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class ElementEmptyDeclarationEntry : ElementDeclarationEntry
    {
        public ElementEmptyDeclarationEntry()
        {
            _type = ElementContentType.Empty;
        }

        public override Boolean Check(Element element)
        {
            return element.ChildNodes.Length == 0;
        }
    }
}
