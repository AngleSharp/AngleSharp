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

                for (int i = 0; i < selectors.Count; i++)
                    sum += selectors[i].selector.Specifity;

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
                        sb.Append(selectors[i].selector.Text).Append(selectors[i].delimiter);

                    sb.Append(selectors[n].selector.Text);
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

            if (selectors[last].selector.Match(element))
            {
                if (last > 0)
                    return MatchCascade(last - 1, element);
                else
                    return true;
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
                selectors.Add(new CombinatorSelector { selector = selector, transform = null });
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
                selector = combinator.Change(selector),
                transform = combinator.Transform,
                delimiter = combinator.Delimiter
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
            var elements = selectors[pos].transform(element);

            foreach (var e in elements)
            {
                if (selectors[pos].selector.Match(e))
                {
                    if (pos == 0 || MatchCascade(pos - 1, e))
                        return true;
                }
            }

            return false;
        }

        #endregion

        #region Nested

        struct CombinatorSelector
        {
            public Char delimiter;
            public Func<IElement, IEnumerable<IElement>> transform;
            public ISelector selector;
        }

        #endregion
    }
}
