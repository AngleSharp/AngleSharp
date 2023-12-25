namespace AngleSharp.Html.Parser
{
    using AngleSharp.Browser;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Construction;
    using Text;

    /// <summary>
    /// Represents the interface of an HTML parser.
    /// </summary>
    public interface IHtmlParser : IParser
    {
        /// <summary>
        /// Parses the string and returns the result.
        /// </summary>
        IHtmlDocument ParseDocument(String source);

        /// <summary>
        /// Parses the stream and returns the result.
        /// </summary>
        IHtmlDocument ParseDocument(Stream source);

        /// <summary>
        /// Parses the string and returns the result.
        /// </summary>
        INodeList ParseFragment(String source, IElement contextElement);

        /// <summary>
        /// Parses the stream and returns the result.
        /// </summary>
        INodeList ParseFragment(Stream source, IElement contextElement);

        /// <summary>
        /// Parses the string and returns the head.
        /// </summary>
        IHtmlHeadElement? ParseHead(String source);

        /// <summary>
        /// Parses the stream and returns the head.
        /// </summary>
        IHtmlHeadElement? ParseHead(Stream source);

        /// <summary>
        /// Parses the string asynchronously.
        /// </summary>
        Task<IHtmlDocument> ParseDocumentAsync(String source, CancellationToken cancel);

        /// <summary>
        /// Parses the stream asynchronously.
        /// </summary>
        Task<IHtmlDocument> ParseDocumentAsync(Stream source, CancellationToken cancel);

        /// <summary>
        /// Parses the string asynchronously.
        /// </summary>
        Task<IHtmlHeadElement?> ParseHeadAsync(String source, CancellationToken cancel);

        /// <summary>
        /// Parses the stream asynchronously.
        /// </summary>
        Task<IHtmlHeadElement?> ParseHeadAsync(Stream source, CancellationToken cancel);

        /// <summary>
        /// Populates the given HTML document asynchronously.
        /// </summary>
        Task<IDocument> ParseDocumentAsync(IDocument document, CancellationToken cancel);

        // /// <summary>
        // /// Parses the stream and returns the result.
        // /// </summary>
        // IReadOnlyDocument ParseReadOnlyDocument(IReadOnlyTextSource source, Middleware? middleware = null);
        /// <summary>
        /// Parses the read only text source and returns the result.
        /// </summary>
        /// <param name="chars">Read only chars</param>
        /// <param name="middleware">Tokenizer middleware</param>
        /// <typeparam name="TDocument">Type of document to parse into, should implement <see cref="IConstructableDocument"/></typeparam>
        /// <typeparam name="TElement">Type of element to use for document construction, should implement <see cref="IConstructableElement"/></typeparam>
        /// <returns>Constructed TDocument instance</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no read-only construction factory is found for specified type arguments.
        /// </exception>
        /// <remarks>
        /// This method is intended for use with custom <see cref="IDomConstructionElementFactory{TDocument,TElement}"/> implementations.
        /// </remarks>
        TDocument ParseDocument<TDocument, TElement>(ReadOnlyMemory<Char> chars, TokenizerMiddleware? middleware = null)
            where TDocument : class, IConstructableDocument
            where TElement : class, IConstructableElement;

        /// <summary>
        /// Parses the read only text source and returns the result.
        /// </summary>
        /// <param name="source">Read only text source.</param>
        /// <param name="middleware">Tokenizer middleware</param>
        /// <typeparam name="TDocument">Type of document to parse into, should implement <see cref="IConstructableDocument"/></typeparam>
        /// <typeparam name="TElement">Type of element to use for document construction, should implement <see cref="IConstructableElement"/></typeparam>
        /// <returns>Constructed TDocument instance</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no read-only construction factory is found for specified type arguments.
        /// </exception>
        /// <remarks>
        /// This method is intended for use with custom <see cref="IDomConstructionElementFactory{TDocument,TElement}"/> implementations.
        /// </remarks>
        TDocument ParseDocument<TDocument, TElement>(IReadOnlyTextSource source, TokenizerMiddleware? middleware = null)
            where TDocument : class, IConstructableDocument
            where TElement : class, IConstructableElement;

        /// <summary>
        /// Parses the read only text source and returns the result.
        /// </summary>
        IHtmlDocument ParseDocument(IReadOnlyTextSource source);
    }
}
