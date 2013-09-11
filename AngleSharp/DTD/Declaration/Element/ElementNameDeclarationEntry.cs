using System;

namespace AngleSharp.DTD
{
    sealed class ElementNameDeclarationEntry : ElementQuantifiedDeclarationEntry
    {
        #region Properties

        public String Name
        {
            get;
            set;
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
                if (inspector.Current.NodeName != Name)
                    break;

                inspector.Index++;
                found++;
            }

            return found >= min;
        }

        #endregion
    }
}
