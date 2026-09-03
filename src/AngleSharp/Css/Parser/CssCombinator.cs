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
        /// Gets the traversal performed by the combinator. Matching walks it
        /// through a <see cref="CombinatorCursor"/> instead of a delegate, so
        /// that stepping over a candidate element does not allocate.
        /// </summary>
        public CssCombinatorKind Kind
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

        /// <summary>
        /// Gets the cells sharing the column of the given cell, or null if the
        /// element is not a cell of a table column.
        /// </summary>
        internal static List<IElement>? GetColumnCells(IElement element)
        {
            var table = GetContainingTable(element);

            if (table is null)
            {
                return null;
            }

            var columnIndex = GetColumnIndex(element);

            if (columnIndex < 0)
            {
                return null;
            }

            var rows = GetTableRows(table);
            var cells = new List<IElement>();

            foreach (var row in rows)
            {
                var cell = GetCellAtColumn(row, columnIndex);

                if (cell != null)
                {
                    cells.Add(cell);
                }
            }

            return cells;
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

        private static Int32 GetColumnIndex(IElement cell)
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

            var columnIndex = 0;

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

                        columnIndex += GetColumnSpan(childElement);
                    }
                }
            }

            return -1;
        }

        private static IElement? GetCellAtColumn(IElement row, Int32 columnIndex)
        {
            var tagName = row.LocalName;

            if (tagName != "tr")
            {
                return null;
            }

            var currentIndex = 0;

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

                        currentIndex += GetColumnSpan(childElement);
                    }
                }
            }

            return null;
        }

        private static Int32 GetColumnSpan(IElement cell)
        {
            var colspanAttr = cell.GetAttribute("colspan");

            if (!String.IsNullOrEmpty(colspanAttr) && Int32.TryParse(colspanAttr, out var parsedColspan) && parsedColspan > 0)
            {
                return parsedColspan;
            }

            return 1;
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

        #endregion

        #region Classes

        private sealed class ChildCombinator : CssCombinator
        {
            public ChildCombinator()
            {
                Delimiter = CombinatorSymbols.Child;
                Kind = CssCombinatorKind.Child;
            }
        }

        private sealed class DeepCombinator : CssCombinator
        {
            public DeepCombinator()
            {
                Delimiter = CombinatorSymbols.Deep;
                Kind = CssCombinatorKind.Deep;
            }
        }

        private sealed class DescendantCombinator : CssCombinator
        {
            public DescendantCombinator()
            {
                Delimiter = CombinatorSymbols.Descendant;
                Kind = CssCombinatorKind.Descendant;
            }
        }

        private sealed class AdjacentSiblingCombinator : CssCombinator
        {
            public AdjacentSiblingCombinator()
            {
                Delimiter = CombinatorSymbols.Adjacent;
                Kind = CssCombinatorKind.AdjacentSibling;
            }
        }

        private sealed class SiblingCombinator : CssCombinator
        {
            public SiblingCombinator()
            {
                Delimiter = CombinatorSymbols.Sibling;
                Kind = CssCombinatorKind.Sibling;
            }
        }

        private sealed class NamespaceCombinator : CssCombinator
        {
            public NamespaceCombinator()
            {
                Delimiter = CombinatorSymbols.Pipe;
                Kind = CssCombinatorKind.Namespace;
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
                Kind = CssCombinatorKind.Column;
            }
        }

        #endregion
    }
}
