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
                case Tags.TABLE:
                case Tags.TBODY:
                case Tags.TFOOT:
                case Tags.THEAD:
                case Tags.TR:
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
            return (tagName == Tags.TBODY || tagName == Tags.TFOOT || tagName == Tags.THEAD);
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (td, th).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        public static Boolean IsTableCellElement(this String tagName)
        {
            return (tagName == Tags.TD || tagName == Tags.TH);
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
                case Tags.CAPTION:
                case Tags.COL:
                case Tags.COLGROUP:
                case Tags.TBODY:
                case Tags.TFOOT:
                case Tags.THEAD:
                    return true;

                case Tags.TR:
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
                case Tags.BODY:
                case Tags.HTML:
                case Tags.COLGROUP:
                case Tags.COL:
                case Tags.TH:
                case Tags.TD:
                case Tags.CAPTION:
                    return true;

                case Tags.TR:
                    return includeRow;

                default:
                    return false;
            }
        }
    }
}
