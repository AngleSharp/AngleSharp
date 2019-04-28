namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a complex selector, i.e. one or more compound selectors
    /// separated by combinators.
    /// </summary>
    sealed class ComplexSelector : ISelector
    {
        #region Fields

        private readonly List<CombinatorSelector> _combinators;

        #endregion

        #region ctor

        public ComplexSelector()
        {
            _combinators = new List<CombinatorSelector>();
        }

        #endregion

        #region Properties

        public Priority Specificity
        {
            get
            {
                var sum = new Priority();
                var n = _combinators.Count;

                for (var i = 0; i < n; i++)
                {
                    sum += _combinators[i].Selector.Specificity;
                }

                return sum;
            }
        }

        public String Text
        {
            get
            {
                var parts = new String[2 * _combinators.Count + 1];

                if (_combinators.Count > 0)
                {
                    var l = 0;
                    var n = _combinators.Count - 1;

                    for (var i = 0; i < n; i++)
                    {
                        parts[l++] = _combinators[i].Selector.Text;
                        parts[l++] = _combinators[i].Delimiter;
                    }

                    parts[l] = _combinators[n].Selector.Text;
                }

                return String.Concat(parts);
            }
        }

        public Int32 Length => _combinators.Count;

        public Boolean IsReady
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Accept(ISelectorVisitor visitor)
        {
            var selectors = _combinators.Select(m => m.Selector);
            var symbols = _combinators.Take(_combinators.Count - 1).Select(m => m.Delimiter);
            visitor.Combinator(selectors, symbols);
        }

        public Boolean Match(IElement element, IElement scope)
        {
            var last = _combinators.Count - 1;

            if (_combinators[last].Selector.Match(element, scope))
            {
                return last > 0 ? MatchCascade(last - 1, element, scope) : true;
            }

            return false;
        }

        public void ConcludeSelector(ISelector selector)
        {
            if (!IsReady)
            {
                _combinators.Add(new CombinatorSelector
                {
                    Selector = selector,
                    Transform = null,
                    Delimiter = null
                });
                IsReady = true;
            }
        }

        public void AppendSelector(ISelector selector, CssCombinator combinator)
        {
            if (!IsReady)
            {
                _combinators.Add(new CombinatorSelector
                {
                    Selector = combinator.Change(selector),
                    Transform = combinator.Transform,
                    Delimiter = combinator.Delimiter
                });
            }
        }

        #endregion

        #region Helpers

        private Boolean MatchCascade(Int32 pos, IElement element, IElement scope)
        {
            var newElements = _combinators[pos].Transform(element);

            foreach (var newElement in newElements)
            {
                if (_combinators[pos].Selector.Match(newElement, scope) && (pos == 0 || MatchCascade(pos - 1, newElement, scope)))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Nested Structure

        private struct CombinatorSelector
        {
            public String Delimiter;
            public Func<IElement, IEnumerable<IElement>> Transform;
            public ISelector Selector;
        }

        #endregion
    }
}
