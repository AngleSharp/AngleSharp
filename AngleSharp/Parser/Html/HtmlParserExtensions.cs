namespace AngleSharp.Parser.Html
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Html;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerStepThrough]
    static class HtmlParserExtensions
    {
        /// <summary>
        /// Compares another attribute container to the current container.
        /// </summary>
        /// <param name="sourceAttributes">The original attribute list.</param>
        /// <param name="targetAttributes">The list of attributes to compare to.</param>
        /// <returns>True if both objects are equal, otherwise false.</returns>
        public static Boolean IsEqualTo(this AttrContainer sourceAttributes, AttrContainer targetAttributes)
        {
            if (sourceAttributes.Count == targetAttributes.Count)
            {
                for (int i = 0; i < sourceAttributes.Count; i++)
                {
                    var elA = sourceAttributes[i];
                    var elB = targetAttributes[elA.Name];

                    if (elB == null || elA.Value != elB.Value)
                        return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks for each attribute on the token if the attribute is already present on the node.
        /// If it is not, the attribute and its corresponding value is added to the node.
        /// </summary>
        /// <param name="element">The node with the target attributes.</param>
        /// <param name="attributes">The attributes to set.</param>
        public static void SetAttributes(this Element element, List<KeyValuePair<String, String>> attributes)
        {
            foreach (var attribute in attributes)
                element.AddAttribute(attribute.Key, attribute.Value);
        }

        /// <summary>
        /// Adds an element to the list of active formatting elements.
        /// </summary>
        /// <param name="formatting">The list of formatting elements to modify.</param>
        /// <param name="element">The element to add.</param>
        public static void AddFormatting(this List<Element> formatting, Element element)
        {
            var count = 0;

            for (var i = formatting.Count - 1; i >= 0; i--)
            {
                var format = formatting[i];

                if (format == null)
                    break;

                if (format.NodeName == element.NodeName && format.NamespaceUri == element.NamespaceUri && format.Attributes.IsEqualTo(element.Attributes) && ++count == 3)
                {
                    formatting.RemoveAt(i);
                    break;
                }
            }

            formatting.Add(element);
        }

        /// <summary>
        /// Clear the list of active formatting elements up to the last marker.
        /// </summary>
        /// <param name="formatting">The list of formatting elements to modify.</param>
        public static void ClearFormatting(this List<Element> formatting)
        {
            while (formatting.Count != 0)
            {
                var index = formatting.Count - 1;
                var entry = formatting[index];
                formatting.RemoveAt(index);

                if (entry == null)
                    break;
            }
        }

        /// <summary>
        /// Inserts a scope marker at the end of the list of active formatting elements.
        /// </summary>
        /// <param name="formatting">The list of formatting elements to modify.</param>
        public static void AddScopeMarker(this List<Element> formatting)
        {
            formatting.Add(null);
        }

        /// <summary>
        /// Appends a comment node to the specified node.
        /// </summary>
        /// <param name="parent">The node which will contain the comment node.</param>
        /// <param name="data">The comment.</param>
        public static void AddComment(this Node parent, String data)
        {
            parent.AddNode(new Comment(data) { Owner = parent.Owner });
        }

        /// <summary>
        /// Examines if the given element is one of the table elements (table, tbody, tfoot, thead, tr).
        /// </summary>
        /// <param name="node">The node to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static Boolean IsTableElement(this INode node)
        {
            return (node is IHtmlTableElement || node is IHtmlTableSectionElement || node is IHtmlTableRowElement);
        }
    }
}
