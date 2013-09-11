using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    sealed class ElementMixedDeclarationEntry : ElementQuantifiedDeclarationEntry
    {
        List<String> _names;

        public ElementMixedDeclarationEntry()
        {
            _names = new List<String>();
            _type = ElementContentType.Mixed;
        }

        public List<String> Names
        {
            get { return _names; }
        }

        public override Boolean Check(Element element)
        {
            foreach (var child in element.ChildNodes)
            {
                if (child is TextNode && !_names.Contains("#PCDATA") && !((TextNode)child).IsEmpty)
                    return false;
                else if (child is Element && !_names.Contains(child.NodeName))
                    return false;
            }

            return true;
        }
    }
}
