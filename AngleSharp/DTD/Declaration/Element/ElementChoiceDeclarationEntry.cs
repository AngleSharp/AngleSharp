using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    sealed class ElementChoiceDeclarationEntry : ElementChildrenDeclarationEntry
    {
        #region Properties

        public List<ElementQuantifiedDeclarationEntry> Choice
        {
            get { return _children; }
        }

        #endregion

        #region Methods

        public override Boolean Check(NodeInspector inspector)
        {
            var min = _quantifier == ElementQuantifier.ZeroOrMore || _quantifier == ElementQuantifier.ZeroOrOne ? 0 : 1;
            var max = _quantifier == ElementQuantifier.One || _quantifier == ElementQuantifier.ZeroOrOne ? 1 : Int32.MaxValue;
            var found = 0;

            while (found < max && !inspector.IsCompleted)
            {
                var hit = false;

                foreach (var choice in _children)
                {
                    var previous = inspector.Index;

                    if (choice.Check(inspector))
                    {
                        hit = true;
                        break;
                    }

                    inspector.Index = previous;
                }

                if (hit)
                    found++;
                else
                    break;
            }

            return found >= min;
        }

        #endregion
    }
}
