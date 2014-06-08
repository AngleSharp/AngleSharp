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
            switch (name)
            {
                case "xlink:actuate":
                case "xlink:arcrole":
                case "xlink:href":
                case "xlink:role":
                case "xlink:show":
                case "xlink:title":
                case "xlink:type":
                    element.SetAttribute(Namespaces.XLink, name.Substring(name.IndexOf(':') + 1), value);
                    break;

                case "xml:base":
                case "xml:lang":
                case "xml:space":
                    element.SetAttribute(Namespaces.Xml, name, value);
                    break;

                case "xmlns":
                case "xmlns:xlink":
                    element.SetAttribute(Namespaces.XmlNS, name, value);
                    break;

                default:
                    element.SetAttribute(name, value);
                    break;
            }
        }
    }
}
