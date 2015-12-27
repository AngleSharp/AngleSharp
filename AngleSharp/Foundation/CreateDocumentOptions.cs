namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Dom.Svg;
    using AngleSharp.Dom.Xml;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Data transport class to abstract common options in document creation.
    /// </summary>
    sealed class CreateDocumentOptions
    {
        #region Fields

        readonly IResponse _response;
        readonly MimeType _contentType;
        readonly TextSource _source;

        #endregion

        #region ctor

        /// <summary>
        /// Creates new document creation options.
        /// </summary>
        /// <param name="response">The response to hand over.</param>
        /// <param name="source">The source of the document.</param>
        public CreateDocumentOptions(IResponse response, TextSource source)
            : this(response, response.GetContentType(MimeTypes.Html), source)
        {
        }

        /// <summary>
        /// Creates new document creation options. Selects the source from the
        /// response by potentially using the encoding from the configuration.
        /// </summary>
        /// <param name="response">The response to hand over.</param>
        /// <param name="configuration">The configuration to use.</param>
        public CreateDocumentOptions(IResponse response, IConfiguration configuration)
        {
            var contentType = response.GetContentType(MimeTypes.Html);
            var encoding = configuration.DefaultEncoding();
            var charset = contentType.GetParameter(AttributeNames.Charset);

            if (!String.IsNullOrEmpty(charset) && TextEncoding.IsSupported(charset))
                encoding = TextEncoding.Resolve(charset);

            _source = new TextSource(response.Content, encoding);
            _contentType = contentType;
            _response = response;
        }

        /// <summary>
        /// Creates new document creation options.
        /// </summary>
        /// <param name="response">The response to hand over.</param>
        /// <param name="contentType">The content mime-type.</param>
        /// <param name="source">The source of the document.</param>
        public CreateDocumentOptions(IResponse response, MimeType contentType, TextSource source)
        {
            _response = response;
            _contentType = contentType;
            _source = source;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the response to handle.
        /// </summary>
        public IResponse Response
        {
            get { return _response; }
        }

        /// <summary>
        /// Gets or sets the mime-type of the response.
        /// </summary>
        public MimeType ContentType
        {
            get { return _contentType; }
        }

        /// <summary>
        /// Gets or sets the text source to provide.
        /// </summary>
        public TextSource Source
        {
            get { return _source; }
        }

        /// <summary>
        /// Gets or sets the import ancestor, if any.
        /// </summary>
        public IDocument ImportAncestor 
        { 
            get; 
            set; 
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to find the right creator, otherwise returns the HTML
        /// document creation delegate.
        /// </summary>
        /// <returns>The delegate to create a new document instance.</returns>
        public Func<IBrowsingContext, CreateDocumentOptions, CancellationToken, Task<IDocument>> FindCreator()
        {
            if (_contentType.Represents(MimeTypes.Xml) || _contentType.Represents(MimeTypes.ApplicationXml))
            {
                return XmlDocument.LoadAsync;
            }
            else if (_contentType.Represents(MimeTypes.Svg))
            {
                return SvgDocument.LoadAsync;
            }

            return HtmlDocument.LoadAsync;
        }

        #endregion
    }
}
