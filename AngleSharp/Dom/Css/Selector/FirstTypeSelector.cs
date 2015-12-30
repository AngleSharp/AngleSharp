namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// The nth-of-type selector.
    /// </summary>
    sealed class FirstTypeSelector : ChildSelector
    {
        public FirstTypeSelector()
            : base(PseudoClassNames.NthOfType)
        {
        }

        public override Boolean Match(IElement element)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                var n = Math.Sign(_step);
                var k = 0;

                for (var i = 0; i < parent.ChildNodes.Length; i++)
                {
                    var child = parent.ChildNodes[i] as IElement;

                    if (child != null && child.NodeName.Is(element.NodeName))
                    {
                        k += 1;

                        if (child == element)
                        {
                            var diff = k - _offset;
                            return diff == 0 || (Math.Sign(diff) == n && diff % _step == 0);
                        }
                    }
                }
            }

            return false;
        }
    }
}
