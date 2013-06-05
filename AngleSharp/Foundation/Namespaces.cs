using System;

namespace AngleSharp
{
    /// <summary>
    /// Contains a list of common namespaces.
    /// </summary>
    static class Namespaces
    {
        #region Members

        static readonly string html = "http://www.w3.org/1999/xhtml";
        static readonly string xlink = "http://www.w3.org/1999/xlink";
        static readonly string xml = "http://www.w3.org/XML/1998/namespace";
        static readonly string xmlns = "http://www.w3.org/2000/xmlns/";
        static readonly string svg = "http://www.w3.org/2000/svg";
        static readonly string mathml = "http://www.w3.org/1998/Math/MathML";
        static readonly string declaration = "xmlns";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the namespace for HTML elements.
        /// </summary>
        public static string Html { get { return html; } }

        /// <summary>
        /// Gets the namespace for XMLNS elements.
        /// </summary>
        public static string XmlNS { get { return xmlns; } }

        /// <summary>
        /// Gets the namespace for XMLNS elements.
        /// </summary>
        public static string XLink { get { return xlink; } }

        /// <summary>
        /// Gets the namespace for XML elements.
        /// </summary>
        public static string Xml { get { return xml; } }

        /// <summary>
        /// Gets the namespace declaration (xmlns).
        /// </summary>
        public static string Declaration { get { return declaration; } }

        /// <summary>
        /// Gets the namespace for SVG elements.
        /// </summary>
        public static string Svg { get { return svg; } }

        /// <summary>
        /// Gets the namespace for MathML elements.
        /// </summary>
        public static string MathML { get { return mathml; } }

        #endregion
    }
}
