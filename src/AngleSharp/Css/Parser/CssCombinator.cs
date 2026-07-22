namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
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
        /// The deep combinator (>>>).
        /// </summary>
        public static readonly CssCombinator Deep = new DeepCombinator();

        /// <summary>
        /// The descendant operator (space, or alternatively >>).
        /// </summary>
        public static readonly CssCombinator Descendant = new DescendantCombinator();

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
        public Func<IElement, IEnumerable<IElement>>? Transform
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the delimiter that represents the combinator.
        /// </summary>
        public String? Delimiter
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
        public virtual ISelector Change(ISelector selector) => selector;

        #endregion

        #region Helpers

        protected static IEnumerable<IElement> Single(IElement? element)
        {
            if (element != null)
            {
                yield return element;
            }
        }

        #endregion

        #region Classes

        private sealed class ChildCombinator : CssCombinator
        {
            public ChildCombinator()
            {
                Delimiter = CombinatorSymbols.Child;
                Transform = el => Single(el.ParentElement);
            }
        }

        private sealed class DeepCombinator : CssCombinator
        {
            public DeepCombinator()
            {
                Delimiter = CombinatorSymbols.Deep;
                Transform = el => Single(el.Parent is IShadowRoot shadowRoot ? shadowRoot.Host : null);
            }
        }

        private sealed class DescendantCombinator : CssCombinator
        {
            public DescendantCombinator()
            {
                Delimiter = CombinatorSymbols.Descendant;
                Transform = GetAncestors;
            }

            private static IEnumerable<IElement> GetAncestors(IElement el)
            {
                var parent = el.ParentElement;

                while (parent != null)
                {
                    yield return parent;
                    parent = parent.ParentElement;
                }
            }
        }

        private sealed class AdjacentSiblingCombinator : CssCombinator
        {
            public AdjacentSiblingCombinator()
            {
                Delimiter = CombinatorSymbols.Adjacent;
                Transform = el => Single(el.PreviousElementSibling);
            }
        }

        private sealed class SiblingCombinator : CssCombinator
        {
            public SiblingCombinator()
            {
                Delimiter = CombinatorSymbols.Sibling;
                Transform = el =>
                {
                    var parent = el.ParentElement;

                    if (parent != null)
                    {
                        var siblings = new List<IElement>();

                        foreach (var child in parent.ChildNodes)
                        {
                            if (child is IElement element)
                            {
                                if (Object.ReferenceEquals(element, el))
                                {
                                    break;
                                }

                                siblings.Add(element);
                            }
                        }

                        return siblings;
                    }

                    return Array.Empty<IElement>();
                };
            }
        }

        private sealed class NamespaceCombinator : CssCombinator
        {
            public NamespaceCombinator()
            {
                Delimiter = CombinatorSymbols.Pipe;
                Transform = Single;
            }

            public override ISelector Change(ISelector selector)
            {
                var prefix = selector switch
                {
                    TypeSelector typeSelector => typeSelector.TypeName,
                    _ => selector.Text
                };

                return new NamespaceSelector(prefix);
            }
        }

        private sealed class ColumnCombinator : CssCombinator
        {
            public ColumnCombinator()
            {
                Delimiter = CombinatorSymbols.Column;
                Transform = el =>
                {
                    var cells = new List<IElement>();
                    var table = GetContainingTable(el);
                    
                    if (table != null)
                    {
                        var columnIndex = GetColumnIndex(el);
                        
                        if (columnIndex >= 0)
                        {
                            var rows = GetTableRows(table);
                            
                            foreach (var row in rows)
                            {
                                var cell = GetCellAtColumn(row, columnIndex);
                                if (cell != null)
                                {
                                    cells.Add(cell);
                                }
                            }
                        }
                    }
                    
                    return cells;
                };
            }

            private static IElement? GetContainingTable(IElement element)
            {
                var current = element.ParentElement;
                
                while (current != null)
                {
                    if (current.LocalName == "table")
                    {
                        return current;
                    }
                    
                    current = current.ParentElement;
                }
                
                return null;
            }

            private static int GetColumnIndex(IElement cell)
            {
                var tagName = cell.LocalName;
                if (tagName != "td" && tagName != "th")
                {
                    return -1;
                }

                var row = cell.ParentElement;
                if (row == null)
                {
                    return -1;
                }

                int columnIndex = 0;
                
                foreach (var child in row.ChildNodes)
                {
                    if (child is IElement childElement)
                    {
                        var childTagName = childElement.LocalName;
                        if (childTagName == "td" || childTagName == "th")
                        {
                            if (Object.ReferenceEquals(childElement, cell))
                            {
                                return columnIndex;
                            }

                            var colspanAttr = childElement.GetAttribute("colspan");
                            var colspan = 1;
                            
                            if (!String.IsNullOrEmpty(colspanAttr) && Int32.TryParse(colspanAttr, out var parsedColspan) && parsedColspan > 0)
                            {
                                colspan = parsedColspan;
                            }

                            columnIndex += colspan;
                        }
                    }
                }

                return -1;
            }

            private static IElement? GetCellAtColumn(IElement row, int columnIndex)
            {
                var tagName = row.LocalName;
                if (tagName != "tr")
                {
                    return null;
                }

                int currentIndex = 0;
                
                foreach (var child in row.ChildNodes)
                {
                    if (child is IElement childElement)
                    {
                        var childTagName = childElement.LocalName;
                        if (childTagName == "td" || childTagName == "th")
                        {
                            if (currentIndex == columnIndex)
                            {
                                return childElement;
                            }

                            var colspanAttr = childElement.GetAttribute("colspan");
                            var colspan = 1;
                            
                            if (!String.IsNullOrEmpty(colspanAttr) && Int32.TryParse(colspanAttr, out var parsedColspan) && parsedColspan > 0)
                            {
                                colspan = parsedColspan;
                            }

                            currentIndex += colspan;
                        }
                    }
                }

                return null;
            }

            private static List<IElement> GetTableRows(IElement table)
            {
                var rows = new List<IElement>();
                
                // Direct tr children
                foreach (var child in table.ChildNodes)
                {
                    if (child is IElement element && element.LocalName == "tr")
                    {
                        rows.Add(element);
                    }
                }
                
                // tr children within thead, tbody, tfoot
                var sections = new[] { "thead", "tbody", "tfoot" };
                foreach (var section in sections)
                {
                    foreach (var child in table.ChildNodes)
                    {
                        if (child is IElement sectionElement && sectionElement.LocalName == section)
                        {
                            foreach (var sectionChild in sectionElement.ChildNodes)
                            {
                                if (sectionChild is IElement rowElement && rowElement.LocalName == "tr")
                                {
                                    rows.Add(rowElement);
                                }
                            }
                        }
                    }
                }
                
                return rows;
            }
        }

        #endregion
    }
}
