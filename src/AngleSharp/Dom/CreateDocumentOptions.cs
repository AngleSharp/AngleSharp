namespace AngleSharp.Dom
{
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;
    using System.Text;

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
        /// <param name="encoding">The optional default encoding.</param>
        /// <param name="ancestor">The optional import ancestor.</param>
        public CreateDocumentOptions(IResponse response, Encoding encoding = null, IDocument ancestor = null)
        {
            var contentType = response.GetContentType(MimeTypeNames.Html);
            var charset = contentType.GetParameter(AttributeNames.Charset);
            var defaultEncoding = encoding ?? Encoding.UTF8;
            var source = new TextSource(response.Content, defaultEncoding);

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
        public IResponse Response => _response;

        /// <summary>
        /// Gets the provided content-type.
        /// </summary>
        public MimeType ContentType => _contentType;

        /// <summary>
        /// Gets the text source that came with the response.
        /// </summary>
        public TextSource Source => _source;

        /// <summary>
        /// Gets the import ancestor, if any.
        /// </summary>
        public IDocument ImportAncestor => _ancestor;

        #endregion
    }
}
