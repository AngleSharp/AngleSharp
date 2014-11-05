namespace AngleSharp.Parser.Html
{
    using AngleSharp.Html;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Useful helpers for the HTML parser.
    /// </summary>
    static class HtmlHelpers
    {
        /// <summary>
        /// Examines if the given element is one of the table elements (table, tbody, tfoot, thead, tr).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        public static Boolean IsTableElement(this String tagName)
        {
            return (tagName.Equals(Tags.Tr) || tagName.Equals(Tags.Table) || tagName.IsTableSectionElement());
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (tbody, tfoot, thead).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        public static Boolean IsTableSectionElement(this String tagName)
        {
            return (tagName.Equals(Tags.Tbody) || tagName.Equals(Tags.Tfoot) || tagName.Equals(Tags.Thead));
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (td, th).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        public static Boolean IsTableCellElement(this String tagName)
        {
            return (tagName.Equals(Tags.Td) || tagName.Equals(Tags.Th));
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (caption, col, colgroup, tbody, tfoot, thead).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <param name="includeRow">True if the tr element should also be tested.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        public static Boolean IsGeneralTableElement(this String tagName, Boolean includeRow = false)
        {
            if (tagName.IsTableSectionElement() || tagName.Equals(Tags.Caption) || tagName.Equals(Tags.Col) || tagName.Equals(Tags.Colgroup))
                return true;
            else if (tagName.Equals(Tags.Tr))
                return includeRow;
            else
                return false;
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (body, caption, col, colgroup, html, td, th).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <param name="includeRow">True if the tr element should also be tested.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        public static Boolean IsSpecialTableElement(this String tagName, Boolean includeRow = false)
        {
            if (tagName.IsTableCellElement() || tagName.Equals(Tags.Body) || tagName.Equals(Tags.Html) || tagName.Equals(Tags.Caption) || tagName.Equals(Tags.Col) || tagName.Equals(Tags.Colgroup))
                return true;
            else if (tagName.Equals(Tags.Tr))
                return includeRow;
            else
                return false;
        }
    }
}
