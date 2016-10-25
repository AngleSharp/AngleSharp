namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// The nth-lastchild selector.
    /// </summary>
    sealed class LastChildSelector : ChildSelector
    {
        public LastChildSelector(Int32 step, Int32 offset, ISelector kind)
            : base(PseudoClassNames.NthLastChild, step, offset, kind)
        {
        }

        public override Boolean Match(IElement element, IElement scope)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                var n = Math.Sign(Step);
                var k = 0;

                for (var i = parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    var child = parent.ChildNodes[i] as IElement;

                    if (child != null && Kind.Match(child, scope))
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
