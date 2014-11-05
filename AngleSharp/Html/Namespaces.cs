namespace AngleSharp.Html
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains a list of common namespaces.
    /// </summary>
    static class Namespaces
    {
        #region Fields

        static readonly Dictionary<String, String> _namespaces;

        #endregion

        #region ctor

        static Namespaces()
        {
            _namespaces = new Dictionary<String, String>();
            _namespaces.Add(HtmlPrefix, HtmlUri);
            _namespaces.Add(XLinkPrefix, XLinkUri);
            _namespaces.Add(XmlPrefix, XmlUri);
            _namespaces.Add(XmlNsPrefix, XmlNsUri);
            _namespaces.Add(SvgPrefix, SvgUri);
            _namespaces.Add(MathMlPrefix, MathMlUri);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the namespace for HTML elements.
        /// </summary>
        public static readonly String HtmlUri = "http://www.w3.org/1999/xhtml";

        /// <summary>
        /// Gets the namespace for XMLNS elements.
        /// </summary>
        public static readonly String XmlNsUri = "http://www.w3.org/2000/xmlns/";

        /// <summary>
        /// Gets the namespace for XMLNS elements.
        /// </summary>
        public static readonly String XLinkUri = "http://www.w3.org/1999/xlink";

        /// <summary>
        /// Gets the namespace for XML elements.
        /// </summary>
        public static readonly String XmlUri = "http://www.w3.org/XML/1998/namespace";

        /// <summary>
        /// Gets the namespace for SVG elements.
        /// </summary>
        public static readonly String SvgUri = "http://www.w3.org/2000/svg";

        /// <summary>
        /// Gets the namespace for MathML elements.
        /// </summary>
        public static readonly String MathMlUri = "http://www.w3.org/1998/Math/MathML";

        /// <summary>
        /// Gets the prefix for HTML elements.
        /// </summary>
        public static readonly String HtmlPrefix = "html";

        /// <summary>
        /// Gets the prefix for XMLNS elements.
        /// </summary>
        public static readonly String XmlNsPrefix = "xmlns";

        /// <summary>
        /// Gets the prefix for XMLNS elements.
        /// </summary>
        public static readonly String XLinkPrefix = "xlink";

        /// <summary>
        /// Gets the prefix for XML elements.
        /// </summary>
        public static readonly String XmlPrefix = "xml";

        /// <summary>
        /// Gets the prefix for SVG elements.
        /// </summary>
        public static readonly String SvgPrefix = "svg";

        /// <summary>
        /// Gets the prefix for MathML elements.
        /// </summary>
        public static readonly String MathMlPrefix = "mathml";

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
                return XmlNsPrefix;

            return String.Concat(XmlNsPrefix, ":", prefix);
        }

        /// <summary>
        /// Gets the namespace URI for the given prefix.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI if any, otherwise NULL.</returns>
        public static String LookupNamespaceUri(String prefix)
        {
            if (_namespaces.ContainsKey(prefix))
                return _namespaces[prefix];

            return null;
        }

        #endregion
    }
}
