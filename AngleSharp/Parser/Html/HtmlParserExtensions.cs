namespace AngleSharp.Parser.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
#if !LEGACY
    using System.Runtime.CompilerServices;
#endif

    /// <summary>
    /// Extensions to be used exclusively by the parser or the tokenizer.
    /// </summary>
    [DebuggerStepThrough]
    static class HtmlParserExtensions
    {
        /// <summary>
        /// Adds all given attributes to the element, without any duplicate checks.
        /// </summary>
        /// <param name="element">The node with the target attributes.</param>
        /// <param name="attributes">The attributes to set.</param>
        public static void SetAttributes(this Element element, List<KeyValuePair<String, String>> attributes)
        {
            for (var i = 0; i < attributes.Count; i++)
            {
                var attribute = attributes[i];
                element.Attributes.Add(new Attr(element, attribute.Key, attribute.Value));
                element.AttributeChanged(attribute.Key, null, null, true);
            }
        }

        /// <summary>
        /// Retrieves a number describing the error of a given error code.
        /// </summary>
        /// <param name="code">A specific error code.</param>
        /// <returns>The code of the error.</returns>
        public static Int32 GetCode(this HtmlParseError code)
        {
            return (Int32)code;
        }

        /// <summary>
        /// Sanatizes the given list by removing the duplicates first, then calls the
        /// SetAttributes method to add the remaining attributes to the element.
        /// </summary>
        /// <param name="element">The node with the target attributes.</param>
        /// <param name="attributes">The attributes to sanatize and set.</param>
        public static void SetUniqueAttributes(this Element element, List<KeyValuePair<String, String>> attributes)
        {
            for (int i = attributes.Count - 1; i >= 0; i--)
            {
                if (element.HasAttribute(attributes[i].Key))
                    attributes.RemoveAt(i);
            }

            element.SetAttributes(attributes);
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

                if (format.NodeName == element.NodeName && format.NamespaceUri == element.NamespaceUri && format.Attributes.AreEqual(element.Attributes) && ++count == 3)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddScopeMarker(this List<Element> formatting)
        {
            formatting.Add(null);
        }

        /// <summary>
        /// Appends a comment node to the specified node.
        /// </summary>
        /// <param name="parent">The node which will contain the comment node.</param>
        /// <param name="token">The comment token.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComment(this Node parent, HtmlToken token)
        {
            parent.AddNode(new Comment(parent.Owner, token.Data));
        }

        /// <summary>
        /// Examines if the given element is one of the table elements (table, tbody, tfoot, thead, tr).
        /// </summary>
        /// <param name="node">The node to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsTableElement(this INode node)
        {
            return (node is IHtmlTableElement || node is IHtmlTableSectionElement || node is IHtmlTableRowElement);
        }
    }
}
