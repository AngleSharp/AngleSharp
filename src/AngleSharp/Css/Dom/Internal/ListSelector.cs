namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a group of selectors, i.e., zero or more selectors separated
    /// by commas.
    /// </summary>
    sealed class ListSelector : Selectors, ISelector, IMultiSelector
    {
        // Lazily built, specificity-descending copy of _selectors, dropped by Invalidate()
        // whenever the list changes. GetMatchingSelector used to re-sort on every single call -
        // allocating an ordered enumerable, its buffer and a key array each time, on top of an
        // O(n) specificity recompute per key - even though the list is immutable once parsed.
        // Mirrors the precedent in AngleSharp.Css's CssStyleRule.ISelectorVisitor.List, which
        // sorts its own selector list once for the same reason.
        private ISelector[]? _sortedBySpecificity;

        public void Accept(ISelectorVisitor visitor)
        {
            visitor.List(_selectors);
        }

        public Boolean Match(IElement element, IElement? scope)
        {
            for (var i = 0; i < _selectors.Count; i++)
            {
                if (_selectors[i].Match(element, scope))
                {
                    return true;
                }
            }

            return false;
        }

        public ISelector? GetMatchingSelector(IElement element, IElement? scope = null)
        {
            // OrderByDescending is stable, so selectors of equal specificity keep their declared
            // order - same as when this ran unsorted-then-sorted on every call.
            var sorted = _sortedBySpecificity ??= _selectors.OrderByDescending(m => m.Specificity).ToArray();

            for (var i = 0; i < sorted.Length; i++)
            {
                if (sorted[i].Match(element, scope))
                {
                    return sorted[i];
                }
            }

            return null;
        }

        protected override void Invalidate()
        {
            base.Invalidate();
            _sortedBySpecificity = null;
        }

        protected override String Stringify()
        {
            var parts = new String[_selectors.Count];

            for (var i = 0; i < _selectors.Count; i++)
            {
                parts[i] = _selectors[i].Text;
            }

            return String.Join(", ", parts);
        }

        protected override Priority ComputeSpecificity()
        {
            var max = Priority.Zero;

            for (var i = 0; i < _selectors.Count; i++)
            {
                var value = _selectors[i].Specificity;

                if (value > max)
                {
                    max = value;
                }
            }

            return max;
        }
    }
}
