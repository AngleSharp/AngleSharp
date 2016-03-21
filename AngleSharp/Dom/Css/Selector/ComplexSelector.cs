namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents a complex selector, i.e. one or more compound selectors
    /// separated by combinators.
    /// </summary>
    sealed class ComplexSelector : CssNode, ISelector
    {
        #region Fields

        readonly List<CombinatorSelector> _selectors;

        #endregion

        #region ctor

        public ComplexSelector()
        {
            _selectors = new List<CombinatorSelector>();
        }

        #endregion

        #region Properties

        public Priority Specifity
        {
            get
            {
                var sum = new Priority();
                var n = _selectors.Count;

                for (var i = 0; i < n; i++)
                {
                    sum += _selectors[i].Selector.Specifity;
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
            get { return _selectors.Count; }
        }

        public Boolean IsReady
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (_selectors.Count > 0)
            {
                var n = _selectors.Count - 1;

                for (var i = 0; i < n; i++)
                {
                    writer.Write(_selectors[i].Selector.Text);
                    writer.Write(_selectors[i].Delimiter);
                }

                writer.Write(_selectors[n].Selector.Text);
            }
        }

        public Boolean Match(IElement element)
        {
            var last = _selectors.Count - 1;

            if (_selectors[last].Selector.Match(element))
            {
                return last > 0 ? MatchCascade(last - 1, element) : true;
            }

            return false;
        }

        public void ConcludeSelector(ISelector selector)
        {
            if (!IsReady)
            {
                _selectors.Add(new CombinatorSelector
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
                _selectors.Add(new CombinatorSelector
                {
                    Selector = combinator.Change(selector),
                    Transform = combinator.Transform,
                    Delimiter = combinator.Delimiter
                });
            }
        }

        #endregion

        #region Helpers

        Boolean MatchCascade(Int32 pos, IElement element)
        {
            var newElements = _selectors[pos].Transform(element);

            foreach (var newElement in newElements)
            {
                if (_selectors[pos].Selector.Match(newElement))
                {
                    if (pos == 0 || MatchCascade(pos - 1, newElement))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region Nested Structure

        struct CombinatorSelector
        {
            public String Delimiter;
            public Func<IElement, IEnumerable<IElement>> Transform;
            public ISelector Selector;
        }

        #endregion
    }
}
