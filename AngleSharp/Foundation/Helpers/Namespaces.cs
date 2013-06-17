using System;

namespace AngleSharp
{
    /// <summary>
    /// Contains a list of common namespaces.
    /// </summary>
    static class Namespaces
    {
        #region Members

        static readonly String html = "http://www.w3.org/1999/xhtml";
        static readonly String xlink = "http://www.w3.org/1999/xlink";
        static readonly String xml = "http://www.w3.org/XML/1998/namespace";
        static readonly String xmlns = "http://www.w3.org/2000/xmlns/";
        static readonly String svg = "http://www.w3.org/2000/svg";
        static readonly String mathml = "http://www.w3.org/1998/Math/MathML";
        static readonly String declaration = "xmlns";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the namespace for HTML elements.
        /// </summary>
        public static String Html 
        { 
            get { return html; } 
        }

        /// <summary>
        /// Gets the namespace for XMLNS elements.
        /// </summary>
        public static String XmlNS 
        { 
            get { return xmlns; } 
        }

        /// <summary>
        /// Gets the namespace for XMLNS elements.
        /// </summary>
        public static String XLink
        {
            get { return xlink; }
        }

        /// <summary>
        /// Gets the namespace for XML elements.
        /// </summary>
        public static String Xml
        { 
            get { return xml; }
        }

        /// <summary>
        /// Gets the namespace declaration (xmlns).
        /// </summary>
        public static String Declaration
        { 
            get { return declaration; }
        }

        /// <summary>
        /// Gets the namespace for SVG elements.
        /// </summary>
        public static String Svg
        { 
            get { return svg; }
        }

        /// <summary>
        /// Gets the namespace for MathML elements.
        /// </summary>
        public static String MathML
        { 
            get { return mathml; } 
        }

        #endregion
    }
}
