using AngleSharp.Xml;
using System;

namespace AngleSharp.DOM.Xml
{
    /// <summary>
    /// Represents a document node that contains only XML nodes.
    /// </summary>
    [DOM("XMLDocument")]
    public sealed class XMLDocument : Document
    {
        internal XMLDocument()
        {
            _contentType = MimeTypes.Xml;
        }

        /// <summary>
        /// Gets if the document is actually valid. This is always
        /// true if the validating option is set - then non-valid
        /// documents are rejected by directly throwing exceptions.
        /// </summary>
        public Boolean IsValid 
        {
            get
            {
                if (Options.IsValidating)
                    return true;

                return XmlValidator.Run(this);
            }
        }
    }
}
