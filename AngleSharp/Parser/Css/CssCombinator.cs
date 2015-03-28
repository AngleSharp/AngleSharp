namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An enumeration with possible CSS combinator values.
    /// </summary>
    abstract class CssCombinator
    {
        #region Fields

        /// <summary>
        /// The child operator (>).
        /// </summary>
        public static readonly CssCombinator Child = new ChildCombinator();

        /// <summary>
        /// The descendent operator (space).
        /// </summary>
        public static readonly CssCombinator Descendent = new DescendentCombinator();

        /// <summary>
        /// The adjacent sibling combinator +.
        /// </summary>
        public static readonly CssCombinator AdjacentSibling = new AdjacentSiblingCombinator();

        /// <summary>
        /// The sibling combinator ~.
        /// </summary>
        public static readonly CssCombinator Sibling = new SiblingCombinator();

        /// <summary>
        /// The namespace combinator |.
        /// </summary>
        public static readonly CssCombinator Namespace = new NamespaceCombinator();

        /// <summary>
        /// The column combinator ||.
        /// </summary>
        public static readonly CssCombinator Column = new ColumnCombinator();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the transformation function for the combinator.
        /// </summary>
        public Func<IElement, IEnumerable<IElement>> Transform
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the delimiter that represents the combinator.
        /// </summary>
        public Char Delimiter
        {
            get;
            protected set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Changes the selector on the LHS according to some special rules.
        /// </summary>
        /// <param name="selector">The original selector.</param>
        /// <returns>The modified (or unmodified) selector.</returns>
        public virtual ISelector Change(ISelector selector)
        {
            return selector;
        }

        #endregion

        #region Helpers

        protected static IEnumerable<IElement> Single(IElement element)
        {
            if (element == null)
                yield break;

            yield return element;
        }

        #endregion

        #region Classes

        sealed class ChildCombinator : CssCombinator
        {
            public ChildCombinator()
            {
                Delimiter = Symbols.GreaterThan;
                Transform = el => Single(el.ParentElement);
            }
        }

        sealed class DescendentCombinator : CssCombinator
        {
            public DescendentCombinator()
            {
                Delimiter = Symbols.Space;
                Transform = el =>
                {
                    var parents = new List<IElement>();
                    var parent = el.ParentElement;

                    while (parent != null)
                    {
                        parents.Add(parent);
                        parent = parent.ParentElement;
                    }

                    return parents;
                };
            }
        }

        sealed class AdjacentSiblingCombinator : CssCombinator
        {
            public AdjacentSiblingCombinator()
            {
                Delimiter = Symbols.Plus;
                Transform = el => Single(el.PreviousElementSibling);
            }
        }

        sealed class SiblingCombinator : CssCombinator
        {
            public SiblingCombinator()
            {
                Delimiter = Symbols.Tilde;
                Transform = el =>
                {
                    var parent = el.ParentElement;

                    if (parent == null)
                        return new IElement[0];

                    var siblings = new List<IElement>();

                    foreach (var child in parent.ChildNodes)
                    {
                        var element = child as IElement;

                        if (element == null)
                            continue;
                        else if (Object.ReferenceEquals(element, el))
                            break;
                        else
                            siblings.Add(element);
                    }

                    return siblings;
                };
            }
        }

        sealed class NamespaceCombinator : CssCombinator
        {
            public NamespaceCombinator()
            {
                Delimiter = Symbols.Pipe;
                Transform = el => Single(el);
            }

            public override ISelector Change(ISelector selector)
            {
                var prefix = selector.Text;
                return new SimpleSelector(el => el.MatchesCssNamespace(prefix), Priority.Zero, prefix);
            }
        }

        sealed class ColumnCombinator : CssCombinator
        {
            public ColumnCombinator()
            {
                //TODO no real implementation yet
                //see: http://dev.w3.org/csswg/selectors-4/#the-column-combinator
            }
        }

        #endregion
    }
}
