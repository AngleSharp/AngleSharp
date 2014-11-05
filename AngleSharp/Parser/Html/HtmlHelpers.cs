namespace AngleSharp.Parser.Html
{
    using AngleSharp.Html;
    using System;
    using System.Diagnostics;
#if LEGACY
    using AngleSharp.Extensions;
#else
    using System.Runtime.CompilerServices;
#endif

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
            return (tagName == Tags.Tr || tagName == Tags.Table || tagName.IsTableSectionElement());
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (tbody, tfoot, thead).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            if (tagName == Tags.Tr)
                return includeRow;
            else if (tagName.IsTableSectionElement() || tagName == Tags.Caption || tagName == Tags.Col || tagName == Tags.Colgroup)
                return true;
            
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
            if (tagName == Tags.Tr)
                return includeRow;
            else if (tagName.IsTableCellElement() || tagName == Tags.Body || tagName == Tags.Html || tagName == Tags.Caption || tagName == Tags.Col || tagName == Tags.Colgroup)
                return true;
            
            return false;
        }
    }
}
