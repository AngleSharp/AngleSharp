namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// The nth-child selector.
    /// </summary>
    sealed class FirstChildSelector : ChildSelector
    {
        public FirstChildSelector(Int32 step, Int32 offset, ISelector kind)
            : base(PseudoClassNames.NthChild, step, offset, kind)
        {
        }

        public override Boolean Match(IElement element)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                var n = Math.Sign(Step);
                var k = 0;

                for (var i = 0; i < parent.ChildNodes.Length; i++)
                {
                    var child = parent.ChildNodes[i] as IElement;

                    if (child != null && Kind.Match(child))
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
