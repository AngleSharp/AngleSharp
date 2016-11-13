namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.IO;

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

        public Priority Specifity
        {
            get
            {
                var sum = new Priority();
                var n = _combinators.Count;

                for (var i = 0; i < n; i++)
                {
                    sum += _combinators[i].Selector.Specifity;
                }

                return sum;
            }
        }

        public String Text
        {
            get { return this.ToCss(); }
        }

        public Int32 Length
        {
            get { return _combinators.Count; }
        }

        public Boolean IsReady
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (_combinators.Count > 0)
            {
                var n = _combinators.Count - 1;

                for (var i = 0; i < n; i++)
                {
                    writer.Write(_combinators[i].Selector.Text);
                    writer.Write(_combinators[i].Delimiter);
                }

                writer.Write(_combinators[n].Selector.Text);
            }
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
                if (_combinators[pos].Selector.Match(newElement, scope))
                {
                    if (pos == 0 || MatchCascade(pos - 1, newElement, scope))
                    {
                        return true;
                    }
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
