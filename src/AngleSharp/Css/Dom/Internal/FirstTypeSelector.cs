namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// The nth-of-type selector.
    /// </summary>
    sealed class FirstTypeSelector : ChildSelector, ISelector
    {
        public FirstTypeSelector(Int32 step, Int32 offset, ISelector kind)
            : base(PseudoClassNames.NthOfType, step, offset, kind)
        {
        }

        public Boolean Match(IElement element, IElement? scope)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                // remove interface dispatch overhead
                if (parent.ChildNodes is NodeList nodeList)
                {
                    return DoMatch(new ConcreteNodeListAccessor(nodeList), element);
                }

                return DoMatch(new InterfaceNodeListAccessor(parent.ChildNodes), element);
            }

            return false;
        }

        private Boolean DoMatch<T>(T nodes, IElement element) where T : INodeListAccessor
        {
            var k = 0;
            var step = Step;
            var n = Math.Sign(step);
            var offset = Offset;
            var length = nodes.Length;
            var nodeName = element.NodeName;

            for (var i = 0; i < length; i++)
            {
                if (nodes[i] is IElement child && child.NodeName.Is(nodeName))
                {
                    k += 1;

                    if (child == element)
                    {
                        var diff = k - offset;
                        return diff == 0 || (Math.Sign(diff) == n && diff % step == 0);
                    }
                }
            }

            return false;
        }
    }
}