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
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
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
                    element.SetAttributeNode(new Attr(GetName(name), value,  Namespaces.XLink));
                    break;

                case "xml:base":
                case "xml:lang":
                case "xml:space":
                    element.SetAttributeNode(new Attr(name, value, Namespaces.Xml));
                    break;

                case "xmlns":
                    element.SetAttributeNS(Namespaces.XmlNS, name, value);
                    break;

                case "xmlns:xlink":
                    element.SetAttributeNode(new Attr(name, value, Namespaces.XmlNS));
                    break;

                default:
                    element.SetAttribute(name, value);
                    break;
            }
        }

        static String GetName(String name)
        {
            return name.Substring(name.IndexOf(':') + 1);
        }
    }
}
