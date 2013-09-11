using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class ElementNameDeclarationEntry : ElementQuantifiedDeclarationEntry
    {
        public String Name
        {
            get;
            set;
        }

        public override Boolean Check(Element element)
        {
            return element.NodeName == Name;
        }
    }
}
