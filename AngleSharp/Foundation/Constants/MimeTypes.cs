using System;

namespace AngleSharp
{
    /// <summary>
    /// Contains a list of common mime-types.
    /// </summary>
    static class MimeTypes
    {
        /// <summary>
        /// Gets the mime-type for HTML text: text/html.
        /// </summary>
        public static readonly String Html = "text/html";

        /// <summary>
        /// Gets the mime-type for XML text: text/xml.
        /// </summary>
        public static readonly String Xml = "text/xml";

        /// <summary>
        /// Gets the mime-type for XML applications, application/xml.
        /// </summary>
        public static readonly String ApplicationXml = "application/xml";

        /// <summary>
        /// Gets the mime-type for XHTML / XML: application/xhtml+xml.
        /// </summary>
        public static readonly String ApplicationXHtml = "application/xhtml+xml";

        /// <summary>
        /// Gets the mime-type for binary data, application/octet-stream.
        /// </summary>
        public static readonly String Binary = "application/octet-stream";

        /// <summary>
        /// Gets a list of mime-types that are recognized as JavaScript.
        /// </summary>
        public static readonly String[] JavaScript = new[] 
        { 
            "application/ecmascript",
            "application/javascript",
            "application/x-ecmascript",
            "application/x-javascript",
            "text/ecmascript",
            "text/javascript",
            "text/javascript1.0",
            "text/javascript1.1",
            "text/javascript1.2",
            "text/javascript1.3",
            "text/javascript1.4",
            "text/javascript1.5",
            "text/jscript",
            "text/livescript",
            "text/x-ecmascript",
            "text/x-javascript"
        };
    }
}
