namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;

    /// <summary>
    /// Data transport class to abstract common options in document creation.
    /// </summary>
    public sealed class CreateDocumentOptions
    {
        #region Fields

        private readonly IResponse _response;
        private readonly MimeType _contentType;
        private readonly TextSource _source;
        private readonly IDocument _ancestor;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new set of document options from the given response with
        /// the provided configuration.
        /// </summary>
        /// <param name="response">The response to pass on.</param>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="ancestor">The optional import ancestor.</param>
        public CreateDocumentOptions(IResponse response, IConfiguration configuration, IDocument ancestor = null)
        {
            var contentType = response.GetContentType(MimeTypeNames.Html);
            var charset = contentType.GetParameter(AttributeNames.Charset);
            var source = new TextSource(response.Content, configuration.DefaultEncoding());

            if (!String.IsNullOrEmpty(charset) && TextEncoding.IsSupported(charset))
            {
                source.CurrentEncoding = TextEncoding.Resolve(charset);
            }

            _source = source;
            _contentType = contentType;
            _response = response;
            _ancestor = ancestor;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the response to create the document for.
        /// </summary>
        public IResponse Response
        {
            get { return _response; }
        }

        /// <summary>
        /// Gets the provided content-type.
        /// </summary>
        public MimeType ContentType
        {
            get { return _contentType; }
        }

        /// <summary>
        /// Gets the text source that came with the response.
        /// </summary>
        public TextSource Source
        {
            get { return _source; }
        }

        /// <summary>
        /// Gets the import ancestor, if any.
        /// </summary>
        public IDocument ImportAncestor 
        { 
            get { return _ancestor; }
        }

        #endregion
    }
}
