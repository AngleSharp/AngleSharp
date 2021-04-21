namespace AngleSharp.Html.Parser
{
    using AngleSharp.Browser;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

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
        /// Parses the string asynchronously.
        /// </summary>
        Task<IHtmlDocument> ParseDocumentAsync(String source, CancellationToken cancel);

        /// <summary>
        /// Parses the stream asynchronously.
        /// </summary>
        Task<IHtmlDocument> ParseDocumentAsync(Stream source, CancellationToken cancel);

        /// <summary>
        /// Populates the given HTML document asynchronously.
        /// </summary>
        Task<IDocument> ParseDocumentAsync(IDocument document, CancellationToken cancel);
    }
}
