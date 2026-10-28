namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents a compound selector, which is a chain of simple selectors
    /// that are not separated by a combinator.
    /// </summary>
    sealed class CompoundSelector : Selectors, ISelector
    {
        // The simple selectors in the order they are tested: as declared, except that the ones
        // containing a :has() go last, so "div:has(img):hover" walks the subtree only for the
        // elements that are already known to be hovered divs. Built on first match and dropped
        // by Invalidate(); publishing a fully built array keeps a racing reader safe.
        private ISelector[]? _matchOrder;

        public Boolean Match(IElement element, IElement? scope)
        {
            var selectors = _matchOrder ??= CreateMatchOrder();

            for (var i = 0; i < selectors.Length; i++)
            {
                if (!selectors[i].Match(element, scope))
                {
                    return false;
                }
            }

            return true;
        }

        public void Accept(ISelectorVisitor visitor)
        {
            visitor.Many(_selectors);
        }

        protected override void Invalidate()
        {
            base.Invalidate();
            _matchOrder = null;
        }

        private ISelector[] CreateMatchOrder()
        {
            var n = _selectors.Count;
            var order = new ISelector[n];
            var deferred = new Boolean[n];
            var k = 0;

            for (var i = 0; i < n; i++)
            {
                deferred[i] = HasSelector.IsContainedIn(_selectors[i]);

                if (!deferred[i])
                {
                    order[k++] = _selectors[i];
                }
            }

            for (var i = 0; i < n; i++)
            {
                if (deferred[i])
                {
                    order[k++] = _selectors[i];
                }
            }

            return order;
        }

        protected override String Stringify()
        {
            var parts = new String[_selectors.Count];

            for (var i = 0; i < _selectors.Count; i++)
            {
                parts[i] = _selectors[i].Text;
            }

            return String.Concat(parts);
        }

        protected override Priority ComputeSpecificity()
        {
            var sum = new Priority();

            for (var i = 0; i < _selectors.Count; i++)
            {
                sum += _selectors[i].Specificity;
            }

            return sum;
        }
    }
}
