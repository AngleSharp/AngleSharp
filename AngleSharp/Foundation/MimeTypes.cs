using System;

namespace AngleSharp
{
    /// <summary>
    /// Contains a list of common mime-types.
    /// </summary>
    static class MimeTypes
    {
        static readonly string html = "text/html";

        /// <summary>
        /// Gets the HTML mime-type: text/html.
        /// </summary>
        public static string Html
        {
            get { return html; }
        }

        static readonly string xml = "text/xml";

        /// <summary>
        /// Gets the XML mime-type: text/xml.
        /// </summary>
        public static string Xml
        {
            get { return xml; }
        }

        static readonly string applicationXml = "application/xml";

        /// <summary>
        /// Gets the XML mime-type: application/xml.
        /// </summary>
        public static string ApplicationXml
        {
            get { return applicationXml; }
        }
    }
}
