using System;

namespace AngleSharp
{
    /// <summary>
    /// Contains a list of common mime-types.
    /// </summary>
    static class MimeTypes
    {
        #region Members

        static readonly String html = "text/html";
        static readonly String xml = "text/xml";
        static readonly String applicationXml = "application/xml";
        static readonly String applicationXHtml = "application/xhtml+xml";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the HTML mime-type: text/html.
        /// </summary>
        public static String Html
        {
            get { return html; }
        }

        /// <summary>
        /// Gets the XML mime-type: text/xml.
        /// </summary>
        public static String Xml
        {
            get { return xml; }
        }

        /// <summary>
        /// Gets the XML mime-type: application/xml.
        /// </summary>
        public static String ApplicationXml
        {
            get { return applicationXml; }
        }

        /// <summary>
        /// Gets the XML mime-type: application/xhtml+xml.
        /// </summary>
        public static String ApplicationXHtml
        {
            get { return applicationXHtml; }
        }

        #endregion
    }
}
