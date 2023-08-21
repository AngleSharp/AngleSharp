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

        protected override Boolean IncludeParameterInSpecificity => true;

        public Boolean Match(IElement element, IElement? scope)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                // remove interface dispatch overhead
                if (parent.ChildNodes is NodeList nodeList)
                {
                    return DoMatch(new ConcreteNodeListAccessor(nodeList), element, scope);
                }

                return DoMatch(new InterfaceNodeListAccessor(parent.ChildNodes), element, scope);
            }

            return false;
        }

        private Boolean DoMatch<T>(T nodes, IElement element, IElement? scope) where T : INodeListAccessor
        {
            var step = Step;
            var n = Math.Sign(step);
            var k = 0;
            var kind = Kind;
            var matchAll = ReferenceEquals(Kind, AllSelector.Instance);
            var offset = Offset;
            var length = nodes.Length;

            for (var i = 0; i < length; i++)
            {
                if (nodes[i] is IElement child && (matchAll || kind.Match(child, scope)))
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
