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

        public override Boolean Check(NodeInspector inspector)
        {
            foreach (var child in _children)
            {
                if (!child.Check(inspector))
                    return false;
            }

            return true;
        }
    }
}
