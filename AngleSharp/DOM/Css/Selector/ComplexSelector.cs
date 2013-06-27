using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Css;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a complex selector.
    /// One or more compound selectors separated by combinators.
    /// </summary>
    internal class ComplexSelector : Selector
    {
        #region Members

        List<CombinatorSelector> selectors;

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
        public override Int32 Specifity
        {
            get
            {
                int sum = 0;

                for (int i = 0; i < selectors.Count; i++)
                    sum += selectors[i].selector.Specifity;

                return sum;
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
        public override Boolean Match(Element element)
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
        public ComplexSelector ConcludeSelector(Selector selector)
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
        public ComplexSelector AppendSelector(Selector selector, CssCombinator combinator)
        {
            if (IsReady)
                return this;

            Func<Element, IEnumerable<Element>> transform = null;
            char delim;

            switch (combinator)
            {
                case CssCombinator.Child:
                    delim = Specification.GT;
                    transform = el => Single(el.ParentElement);
                    break;

                case CssCombinator.AdjacentSibling:
                    delim = Specification.PLUS;
                    transform = el => Single(el.PreviousElementSibling);
                    break;

                case CssCombinator.Descendent:
                    delim = Specification.SPACE;
                    transform = el =>
                    {
                        var parents = new List<Element>();
                        var parent = el.ParentElement;

                        while(parent != null)
                        {
                            parents.Add(parent);
                            parent = parent.ParentElement;
                        }

                        return parents;
                    };
                    break;

                case CssCombinator.Sibling:
                    delim = Specification.TILDE;
                    transform = el =>
                    {
                        var parent = el.ParentElement;

                        if (parent == null)
                            return new Element[0];

                        var kids = parent.Children;
                        var passed = false;
                        var siblings = new List<Element>();

                        for (int i = kids.Length - 1; i >= 0; i--)
			            {
                            if (kids[i] == el)
                                passed = true;
                            else if (passed)
                                siblings.Add(kids[i]);
			            }

                        return siblings;
                    };
                    break;

                default:
                    return this;
            }

            selectors.Add(new CombinatorSelector { selector = selector, transform = transform, delimiter = delim });
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

        Boolean MatchCascade(int pos, Element element)
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

        static IEnumerable<Element> Single(Element element)
        {
            if (element == null)
                return System.Linq.Enumerable.Empty<Element>();

            return new Element[1] { element };
        }

        #endregion

        #region Nested

        struct CombinatorSelector
        {
            public Char delimiter;
            public Func<Element, IEnumerable<Element>> transform;
            public Selector selector;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a valid CSS string representing this selector.
        /// </summary>
        /// <returns>The CSS to create this selector.</returns>
        public override String ToCss()
        {
            var sb = new StringBuilder();

            if (selectors.Count > 0)
            {
                var n = selectors.Count - 1;

                for (int i = 0; i < n; i++)
                    sb.Append(selectors[i].selector.ToCss()).Append(selectors[i].delimiter);

                sb.Append(selectors[n].selector.ToCss());
            }

            return sb.ToString();
        }

        #endregion
    }
}
