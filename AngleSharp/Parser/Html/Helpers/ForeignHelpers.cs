namespace AngleSharp.Parser.Html
{
    using AngleSharp.DOM;
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
                if (String.Compare("actuate", 0, name, 6, 7) == 0 ||
                    String.Compare("arcrole", 0, name, 6, 7) == 0 ||
                    String.Compare("href", 0, name, 6, 4) == 0 ||
                    String.Compare("role", 0, name, 6, 4) == 0 ||
                    String.Compare("show", 0, name, 6, 4) == 0 ||
                    String.Compare("type", 0, name, 6, 4) == 0 ||
                    String.Compare("title", 0, name, 6, 5) == 0)
                {
                    element.SetAttribute(Namespaces.XLink, name.Substring(name.IndexOf(':') + 1), value);
                    return;
                }
            }
            else if (name.Length > 4)
            {
                if (String.Compare("xml:", 0, name, 0, 4) == 0 && (String.Compare("base", 0, name, 4, 4) == 0 ||
                    String.Compare("lang", 0, name, 4, 4) == 0 || String.Compare("space", 0, name, 4, 5) == 0))
                {
                    element.SetAttribute(Namespaces.Xml, name, value);
                    return;
                }
                else if (name.Equals("xmlns") || name.Equals("xmlns:xlink"))
                {
                    element.SetAttribute(Namespaces.XmlNS, name, value);
                    return;
                }
            }

            element.SetAttribute(name, value);
        }
    }
}
