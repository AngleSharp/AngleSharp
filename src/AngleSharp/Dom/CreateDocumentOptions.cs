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

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new set of document options from the given response with
        /// the provided configuration.
        /// </summary>
        /// <param name="response">The response to pass on.</param>
        /// <param name="encoding">The optional default encoding.</param>
        /// <param name="ancestor">The optional import ancestor.</param>
        public CreateDocumentOptions(IResponse response, Encoding? encoding = null, IDocument? ancestor = null)
        {
            var contentType = response.GetContentType(MimeTypeNames.Html);
            var charset = contentType.GetParameter(AttributeNames.Charset);
            var defaultEncoding = encoding ?? Encoding.UTF8;
            var source = new TextSource(response.Content, defaultEncoding);

            if (charset is { Length: > 0 } && TextEncoding.IsSupported(charset))
            {
                source.CurrentEncoding = TextEncoding.Resolve(charset);
            }

            Source = source;
            ContentType = contentType;
            Response = response;
            ImportAncestor = ancestor;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the response to create the document for.
        /// </summary>
        public IResponse Response { get; }

        /// <summary>
        /// Gets the provided content-type.
        /// </summary>
        public MimeType ContentType { get; }

        /// <summary>
        /// Gets the text source that came with the response.
        /// </summary>
        public TextSource Source { get; }

        /// <summary>
        /// Gets the import ancestor, if any.
        /// </summary>
        public IDocument? ImportAncestor { get; }

        #endregion
    }
}
