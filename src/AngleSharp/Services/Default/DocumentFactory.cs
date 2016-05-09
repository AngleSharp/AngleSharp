namespace AngleSharp.Services.Default
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Dom.Svg;
    using AngleSharp.Dom.Xml;
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides the default content-type to document creation mapping.
    /// </summary>
    public sealed class DocumentFactory : IDocumentFactory
    {
        delegate Task<IDocument> Creator(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken);

        readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>
        {
            { MimeTypeNames.Xml, XmlDocument.LoadAsync },
            { MimeTypeNames.ApplicationXml, XmlDocument.LoadAsync },
            { MimeTypeNames.Svg, SvgDocument.LoadAsync },
            { MimeTypeNames.Html, HtmlDocument.LoadAsync },
            { MimeTypeNames.ApplicationXHtml, HtmlDocument.LoadAsync },
            { MimeTypeNames.Plain, HtmlDocument.LoadTextAsync }
        };

        /// <summary>
        /// Creates a new attribute selector from the given arguments.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="options">The options to consider.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The task creating the document from the response.</returns>
        public Task<IDocument> CreateAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var contentType = options.ContentType;

            foreach (var creator in creators)
            {
                if (contentType.Represents(creator.Key))
                {
                    return creator.Value(context, options, cancellationToken);
                }
            }

            return HtmlDocument.LoadAsync(context, options, cancellationToken);
        }
    }
}
