using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    sealed class ElementSequenceDeclarationEntry : ElementChildrenDeclarationEntry
    {
        public List<ElementQuantifiedDeclarationEntry> Sequence
        {
            get { return _children; }
        }

        public override Boolean Check(Element element)
        {
            return false;
        }
    }
}
