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
            if (ReferenceEquals(Kind, AllSelector.Instance))
            {
                return MatchPosition(nodes, element);
            }

            // Inside another of-clause: see NthOfMemo
            if (NthOfMemo.IsNested)
            {
                var position = NthOfMemo.GetPosition(this, nodes, element, scope, fromEnd: true);
                return position > 0 && IsPosition(position);
            }

            return MatchOf(nodes, element, scope);
        }

        // Kept apart from MatchOf, whose try block would cost this loop its optimizations
        private Boolean MatchPosition<T>(T nodes, IElement element) where T : INodeListAccessor
        {
            var step = Step;
            var n = Math.Sign(step);
            var k = 0;
            var offset = Offset;
            var length = nodes.Length;

            for (var i = length - 1; i >= 0; i--)
            {
                if (nodes.TryGetElement(i, out var child))
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

        private Boolean MatchOf<T>(T nodes, IElement element, IElement? scope) where T : INodeListAccessor
        {
            var kind = Kind;
            NthOfMemo.Enter();

            try
            {
                // Outside S the element has no position at all
                if (!kind.Match(element, scope))
                {
                    return false;
                }

                var k = 1;
                var length = nodes.Length;

                for (var i = length - 1; i >= 0; i--)
                {
                    if (nodes.TryGetElement(i, out var child))
                    {
                        if (child == element)
                        {
                            return IsPosition(k);
                        }

                        if (kind.Match(child, scope))
                        {
                            k += 1;
                        }
                    }
                }

                return false;
            }
            finally
            {
                NthOfMemo.Leave();
            }
        }
    }
}