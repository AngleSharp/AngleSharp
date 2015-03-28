namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a complex selector.
    /// One or more compound selectors separated by combinators.
    /// </summary>
    sealed class ComplexSelector : ISelector
    {
        #region Fields

        readonly List<CombinatorSelector> selectors;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new complex selector.
        /// </summary>
        public ComplexSelector()
        {
            selectors = new List<CombinatorSelector>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specifity index for this chain of selectors.
        /// </summary>
        public Priority Specifity
        {
            get
            {
                var sum = new Priority();
                var n = selectors.Count;

                for (int i = 0; i < n; i++)
                {
                    sum += selectors[i].Selector.Specifity;
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

                if (selectors.Count > 0)
                {
                    var n = selectors.Count - 1;

                    for (int i = 0; i < n; i++)
                    {
                        sb.Append(selectors[i].Selector.Text).Append(selectors[i].Delimiter);
                    }

                    sb.Append(selectors[n].Selector.Text);
                }

                return sb.ToPool();
            }
        }

        /// <summary>
        /// Gets the number of selectors in this group.
        /// </summary>
        public Int32 Length
        {
            get { return selectors.Count; }
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
            var last = selectors.Count - 1;

            if (selectors[last].Selector.Match(element))
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
                selectors.Add(new CombinatorSelector
                {
                    Selector = selector,
                    Transform = null,
                    Delimiter = Symbols.Null
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

            selectors.Add(new CombinatorSelector
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
            selectors.Clear();
            return this;
        }

        #endregion

        #region Helpers

        Boolean MatchCascade(Int32 pos, IElement element)
        {
            var newElements = selectors[pos].Transform(element);

            foreach (var newElement in newElements)
            {
                if (selectors[pos].Selector.Match(newElement))
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
            public Char Delimiter;
            public Func<IElement, IEnumerable<IElement>> Transform;
            public ISelector Selector;
        }

        #endregion
    }
}
