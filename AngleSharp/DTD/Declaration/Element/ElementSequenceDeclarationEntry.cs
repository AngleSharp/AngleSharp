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
            var min = _quantifier == ElementQuantifier.ZeroOrMore || _quantifier == ElementQuantifier.ZeroOrOne ? 0 : 1;
            var max = _quantifier == ElementQuantifier.One || _quantifier == ElementQuantifier.ZeroOrOne ? 1 : Int32.MaxValue;
            var found = 0;

            while (found < max && !inspector.IsCompleted)
            {
                var missed = false;
                var previous = inspector.Index;

                foreach (var child in _children)
                {
                    if (!child.Check(inspector))
                    {
                        missed = true;
                        break;
                    }
                }

                if (missed)
                {
                    inspector.Index = previous;
                    break;
                }

                found++;
            }

            return found >= min;
        }
    }
}
