namespace AngleSharp.Parser.Html
{
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
            switch (tagName)
            {
                case Tags.Table:
                case Tags.Tbody:
                case Tags.Tfoot:
                case Tags.Thead:
                case Tags.Tr:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (tbody, tfoot, thead).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        public static Boolean IsTableSectionElement(this String tagName)
        {
            return (tagName == Tags.Tbody || tagName == Tags.Tfoot || tagName == Tags.Thead);
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (td, th).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        public static Boolean IsTableCellElement(this String tagName)
        {
            return (tagName == Tags.Td || tagName == Tags.Th);
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
            switch (tagName)
            {
                case Tags.Caption:
                case Tags.Col:
                case Tags.Colgroup:
                case Tags.Tbody:
                case Tags.Tfoot:
                case Tags.Thead:
                    return true;

                case Tags.Tr:
                    return includeRow;

                default:
                    return false;
            }
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
            switch (tagName)
            {
                case Tags.Body:
                case Tags.Html:
                case Tags.Colgroup:
                case Tags.Col:
                case Tags.Th:
                case Tags.Td:
                case Tags.Caption:
                    return true;

                case Tags.Tr:
                    return includeRow;

                default:
                    return false;
            }
        }
    }
}
