namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The nth-lastchild selector.
    /// </summary>
    sealed class LastChildSelector : ChildSelector, ISelector
    {
        public LastChildSelector(Int32 step, Int32 offset, ISelector kind)
            : base(PseudoClassNames.NthLastChild, step, offset, kind)
        {
        }

        public Boolean Match(IElement element, IElement scope)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                var n = Math.Sign(Step);
                var k = 0;

                for (var i = parent.ChildNodes.Length - 1; i >= 0; i--)
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
