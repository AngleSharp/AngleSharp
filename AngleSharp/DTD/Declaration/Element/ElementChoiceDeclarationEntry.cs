using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    sealed class ElementChoiceDeclarationEntry : ElementChildrenDeclarationEntry
    {
        public List<ElementQuantifiedDeclarationEntry> Choice
        {
            get { return _children; }
        }

        public override Boolean Check(Element element)
        {
            return false;
        }
    }
}
