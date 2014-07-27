namespace AngleSharp.Parser.Html
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using System;

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
        /// <param name="tag">The token with the source attributes.</param>
        public static void AppendAttributes(this Element element, HtmlTagToken tag)
        {
            foreach (var attr in tag.Attributes)
                element.AddAttribute(attr.Key, attr.Value);
        }
    }
}
