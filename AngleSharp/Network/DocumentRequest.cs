namespace AngleSharp.Network
{
    using System;
    using System.IO;
    using AngleSharp.Dom;

    /// <summary>
    /// Represents the arguments to load a document.
    /// </summary>
    public class DocumentRequest
    {
        /// <summary>
        /// Creates a new document request for the given url.
        /// </summary>
        /// <param name="target">The resource's url.</param>
        public DocumentRequest(Url target)
        {
            Target = target;
            Referer = null;
            Method = HttpMethod.Get;
            Body = MemoryStream.Null;
            MimeType = null;
        }

        /// <summary>
        /// Gets or sets the source of the request, if any.
        /// </summary>
        public INode Source
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the target of the request.
        /// </summary>
        public Url Target
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the referrer of the request, if any. The name is
        /// intentionally spelled wrong, to emphasize the relationship with the
        /// HTTP header.
        /// </summary>
        public String Referer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the method to use.
        /// </summary>
        public HttpMethod Method
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the stream of the request's body.
        /// </summary>
        public Stream Body
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the mime-type to use, if any.
        /// </summary>
        public String MimeType
        {
            get;
            set;
        }
    }
}
