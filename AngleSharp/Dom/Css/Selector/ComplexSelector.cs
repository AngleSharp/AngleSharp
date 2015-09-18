namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
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

        readonly List<CombinatorSelector> _selectors;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new complex selector.
        /// </summary>
        public ComplexSelector()
        {
            _selectors = new List<CombinatorSelector>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained nodes.
        /// </summary>
        public IEnumerable<ICssNode> Children
        {
            get { return _selectors.Select(m => m.Selector); }
        }

        /// <summary>
        /// Gets the specifity index for this chain of selectors.
        /// </summary>
        public Priority Specifity
        {
            get
            {
                var sum = new Priority();
                var n = _selectors.Count;

                for (int i = 0; i < n; i++)
                {
                    sum += _selectors[i].Selector.Specifity;
                }

                return sum;
            }
        }

        /// <summary>
        /// Gets the string representation of the selector.
        /// </summary>
        public String Text
        {
            get
            {
                var sb = Pool.NewStringBuilder();

                if (_selectors.Count > 0)
                {
                    var n = _selectors.Count - 1;

                    for (int i = 0; i < n; i++)
                    {
                        sb.Append(_selectors[i].Selector.Text).Append(_selectors[i].Delimiter);
                    }

                    sb.Append(_selectors[n].Selector.Text);
                }

                return sb.ToPool();
            }
        }

        /// <summary>
        /// Gets the number of selectors in this group.
        /// </summary>
        public Int32 Length
        {
            get { return _selectors.Count; }
        }

        /// <summary>
        /// Gets if the selector has already been finalized.
        /// </summary>
        public Boolean IsReady
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given object is matched by this selector.
        /// </summary>
        /// <param name="element">The element to be matched.</param>
        /// <returns>True if the selector matches the given element, otherwise false.</returns>
        public Boolean Match(IElement element)
        {
            var last = _selectors.Count - 1;

            if (_selectors[last].Selector.Match(element))
            {
                return last > 0 ? MatchCascade(last - 1, element) : true;
            }

            return false;
        }

        /// <summary>
        /// Appends the LAST selector to the complex of selectors.
        /// </summary>
        /// <param name="selector">The (final) selector to append.</param>
        /// <returns>The current complex selector.</returns>
        public ComplexSelector ConcludeSelector(ISelector selector)
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

            return this;
        }

        /// <summary>
        /// Appends a selector to the complex of selectors.
        /// </summary>
        /// <param name="selector">The selector to append.</param>
        /// <param name="combinator">The combinator to use.</param>
        /// <returns>The current complex selector.</returns>
        public ComplexSelector AppendSelector(ISelector selector, CssCombinator combinator)
        {
            if (IsReady)
                return this;

            _selectors.Add(new CombinatorSelector
            {
                Selector = combinator.Change(selector),
                Transform = combinator.Transform,
                Delimiter = combinator.Delimiter
            });

            return this;
        }


        /// <summary>
        /// Clears the list of selectors.
        /// </summary>
        /// <returns>The current complex selector.</returns>
        public ComplexSelector ClearSelectors()
        {
            IsReady = false;
            _selectors.Clear();
            return this;
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
                        return true;
                }
            }

            return false;
        }

        #endregion

        #region Nested

        struct CombinatorSelector
        {
            public String Delimiter;
            public Func<IElement, IEnumerable<IElement>> Transform;
            public ISelector Selector;
        }

        #endregion

        #region String Representation

        public String ToCss()
        {
            return Text;
        }

        public String ToCss(IStyleFormatter formatter)
        {
            return ToCss();
        }

        #endregion
    }
}
