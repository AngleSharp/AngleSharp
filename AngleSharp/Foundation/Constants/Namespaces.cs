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

        /// <summary>
        /// Gets the namespace declaration (xmlns).
        /// </summary>
        public static readonly String Declaration = "xmlns";

        #endregion

        #region ctor

        static Namespaces()
        {
            _namespaces = new Dictionary<String, String>();
            _namespaces.Add("html", "http://www.w3.org/1999/xhtml");
            _namespaces.Add("xlink", "http://www.w3.org/1999/xlink");
            _namespaces.Add("xml", "http://www.w3.org/XML/1998/namespace");
            _namespaces.Add("xmlns", "http://www.w3.org/2000/xmlns/");
            _namespaces.Add("svg", "http://www.w3.org/2000/svg");
            _namespaces.Add("mathml", "http://www.w3.org/1998/Math/MathML");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the namespace for HTML elements.
        /// </summary>
        public static String Html 
        { 
            get { return _namespaces["html"]; } 
        }

        /// <summary>
        /// Gets the namespace for XMLNS elements.
        /// </summary>
        public static String XmlNS 
        {
            get { return _namespaces["xmlns"]; } 
        }

        /// <summary>
        /// Gets the namespace for XMLNS elements.
        /// </summary>
        public static String XLink
        {
            get { return _namespaces["xlink"]; } 
        }

        /// <summary>
        /// Gets the namespace for XML elements.
        /// </summary>
        public static String Xml
        {
            get { return _namespaces["xml"]; } 
        }

        /// <summary>
        /// Gets the namespace for SVG elements.
        /// </summary>
        public static String Svg
        { 
            get { return _namespaces["svg"]; } 
        }

        /// <summary>
        /// Gets the namespace for MathML elements.
        /// </summary>
        public static String MathML
        { 
            get { return _namespaces["mathml"]; } 
        }

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
