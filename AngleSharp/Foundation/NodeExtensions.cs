namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Html;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Useful methods for node objects.
    /// </summary>
    static class NodeExtensions
    {
        /// <summary>
        /// Examines if the given element is one of the table elements (table, tbody, tfoot, thead, tr).
        /// </summary>
        /// <param name="node">The node to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [DebuggerStepThrough]
        public static Boolean IsTableElement(this Node node)
        {
            return (node is HTMLTableElement || node is HTMLTableSectionElement || node is HTMLTableRowElement);
        }
    }
}
