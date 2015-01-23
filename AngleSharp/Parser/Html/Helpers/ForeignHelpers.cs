namespace AngleSharp.Parser.Html
{
    using AngleSharp.DOM;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// A collection of useful helpers when working with SVG.
    /// </summary>
    static class ForeignHelpers
    {
        /// <summary>
        /// Adds the attribute with the adjusted prefix, namespace and name.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public static void SetAdjustedAttribute(this Element element, String name, String value)
        {
            if (name.Length > 6 && String.Compare("xlink:", 0, name, 0, 6) == 0)
            {
                if (String.Compare(AttributeNames.Actuate, 0, name, 6, 7) == 0 ||
                    String.Compare(AttributeNames.Arcrole, 0, name, 6, 7) == 0 ||
                    String.Compare(AttributeNames.Href, 0, name, 6, 4) == 0 ||
                    String.Compare(AttributeNames.Role, 0, name, 6, 4) == 0 ||
                    String.Compare(AttributeNames.Show, 0, name, 6, 4) == 0 ||
                    String.Compare(AttributeNames.Type, 0, name, 6, 4) == 0 ||
                    String.Compare(AttributeNames.Title, 0, name, 6, 5) == 0)
                {
                    element.SetAttribute(Namespaces.XLinkUri, name.Substring(name.IndexOf(Symbols.Colon) + 1), value);
                    return;
                }
            }
            else if (name.Length > 4)
            {
                if (String.Compare("xml:", 0, name, 0, 4) == 0 && (String.Compare(Tags.Base, 0, name, 4, 4) == 0 ||
                    String.Compare(AttributeNames.Lang, 0, name, 4, 4) == 0 || String.Compare(AttributeNames.Space, 0, name, 4, 5) == 0))
                {
                    element.SetAttribute(Namespaces.XmlUri, name, value);
                    return;
                }
                else if (name.Equals("xmlns") || name.Equals("xmlns:xlink"))
                {
                    element.SetAttribute(Namespaces.XmlNsUri, name, value);
                    return;
                }
            }

            element.SetAttribute(name, value);
        }
    }
}
