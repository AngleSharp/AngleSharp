namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Dom.Svg;
    using AngleSharp.Dom.Xml;
    using AngleSharp.Network;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Data transport class to abstract common options in document creation.
    /// </summary>
    sealed class CreateDocumentOptions
    {
        /// <summary>
        /// Gets or sets the response to handle.
        /// </summary>
        public IResponse Response { get; set; }

        /// <summary>
        /// Gets or sets the mime-type of the response.
        /// </summary>
        public MimeType ContentType { get; set; }

        /// <summary>
        /// Gets or sets the text source to provide.
        /// </summary>
        public TextSource Source { get; set; }

        /// <summary>
        /// Tries to find the right creator, otherwise returns the HTML
        /// document creation delegate.
        /// </summary>
        /// <returns>The delegate to create a new document instance.</returns>
        public Func<IBrowsingContext, CreateDocumentOptions, CancellationToken, Task<IDocument>> FindCreator()
        {
            if (ContentType.Represents(MimeTypes.Xml) || ContentType.Represents(MimeTypes.ApplicationXml))
            {
                return XmlDocument.LoadAsync;
            }
            else if (ContentType.Represents(MimeTypes.Svg))
            {
                return SvgDocument.LoadAsync;
            }

            return HtmlDocument.LoadAsync;
        }
    }
}
