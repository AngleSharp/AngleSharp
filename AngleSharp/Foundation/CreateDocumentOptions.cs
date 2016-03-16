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

        public CreateDocumentOptions(IResponse response, IConfiguration configuration)
        {
            var contentType = response.GetContentType(MimeTypeNames.Html);
            var encoding = configuration.DefaultEncoding();
            var charset = contentType.GetParameter(AttributeNames.Charset);

            if (!String.IsNullOrEmpty(charset) && TextEncoding.IsSupported(charset))
            {
                encoding = TextEncoding.Resolve(charset);
            }

            _source = new TextSource(response.Content, encoding);
            _contentType = contentType;
            _response = response;
        }

        #endregion

        #region Properties

        public IResponse Response
        {
            get { return _response; }
        }

        public MimeType ContentType
        {
            get { return _contentType; }
        }

        public TextSource Source
        {
            get { return _source; }
        }

        public IDocument ImportAncestor 
        { 
            get; 
            set; 
        }

        #endregion

        #region Methods

        public Func<IBrowsingContext, CreateDocumentOptions, CancellationToken, Task<IDocument>> FindCreator()
        {
            if (_contentType.Represents(MimeTypeNames.Xml) || _contentType.Represents(MimeTypeNames.ApplicationXml))
            {
                return XmlDocument.LoadAsync;
            }
            else if (_contentType.Represents(MimeTypeNames.Svg))
            {
                return SvgDocument.LoadAsync;
            }

            return HtmlDocument.LoadAsync;
        }

        #endregion
    }
}
