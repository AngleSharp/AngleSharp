namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Html;
    using System;
    using System.Collections.Generic;
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

        /// <summary>
        /// Gets a list of HTML elements given by their name attribute.
        /// </summary>
        /// <param name="children">The list to investigate.</param>
        /// <param name="name">The name attribute's value.</param>
        /// <param name="result">The result collection.</param>
        [DebuggerStepThrough]
        public static void GetElementsByName(this NodeList children, String name, List<Element> result)
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] is HTMLElement)
                {
                    var element = (HTMLElement)children[i];

                    if (element.GetAttribute(AttributeNames.Name) == name)
                        result.Add(element);

                    element.ChildNodes.GetElementsByName(name, result);
                }
            }
        }
    }
}
