using System;
using System.Collections.Generic;

namespace AngleSharp
{
    /// <summary>
    /// Contains a list of common namespaces.
    /// </summary>
    static class Namespaces
    {
        #region Members

        static readonly Dictionary<String, String> _namespaces;

        #endregion

        #region ctor

        static Namespaces()
        {
            _namespaces = new Dictionary<String, String>();
            _namespaces.Add("html", Html);
            _namespaces.Add("xlink", XLink);
            _namespaces.Add("xml", Xml);
            _namespaces.Add("xmlns", XmlNS);
            _namespaces.Add("svg", Svg);
            _namespaces.Add("mathml", MathML);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the namespace declaration (xmlns).
        /// </summary>
        public static readonly String Declaration = "xmlns";

        /// <summary>
        /// Gets the namespace for HTML elements.
        /// </summary>
        public static readonly String Html = "http://www.w3.org/1999/xhtml";

        /// <summary>
        /// Gets the namespace for XMLNS elements.
        /// </summary>
        public static readonly String XmlNS = "http://www.w3.org/2000/xmlns/";

        /// <summary>
        /// Gets the namespace for XMLNS elements.
        /// </summary>
        public static readonly String XLink = "http://www.w3.org/1999/xlink";

        /// <summary>
        /// Gets the namespace for XML elements.
        /// </summary>
        public static readonly String Xml = "http://www.w3.org/XML/1998/namespace";

        /// <summary>
        /// Gets the namespace for SVG elements.
        /// </summary>
        public static readonly String Svg = "http://www.w3.org/2000/svg";

        /// <summary>
        /// Gets the namespace for MathML elements.
        /// </summary>
        public static readonly String MathML = "http://www.w3.org/1998/Math/MathML";

        #endregion

        #region Methods

        /// <summary>
        /// Gets the declaration for the given prefix.
        /// </summary>
        /// <param name="prefix">The prefix or null for the default namespace.</param>
        /// <returns>The name for the declaration attribute.</returns>
        public static String DeclarationFor(String prefix)
        {
            if(String.IsNullOrEmpty(prefix))
                return Declaration;

            return Declaration + ":" + prefix;
        }

        /// <summary>
        /// Gets the namespace URI for the given prefix.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI if any, otherwise NULL.</returns>
        public static String LookupNamespaceURI(String prefix)
        {
            if (_namespaces.ContainsKey(prefix))
                return _namespaces[prefix];

            return null;
        }

        #endregion
    }
}
