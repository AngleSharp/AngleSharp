namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The nth-child selector.
    /// </summary>
    sealed class FirstChildSelector : ChildSelector, ISelector
    {
        public FirstChildSelector(Int32 step, Int32 offset, ISelector kind)
            : base(PseudoClassNames.NthChild, step, offset, kind)
        {
        }

        public Boolean Match(IElement element, IElement scope)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                var n = Math.Sign(Step);
                var k = 0;

                for (var i = 0; i < parent.ChildNodes.Length; i++)
                {
                    if (parent.ChildNodes[i] is IElement child && Kind.Match(child, scope))
                    {
                        k += 1;

                        if (child == element)
                        {
                            var diff = k - Offset;
                            return diff == 0 || (Math.Sign(diff) == n && diff % Step == 0);
                        }
                    }
                }
            }

            return false;
        }
    }
}
