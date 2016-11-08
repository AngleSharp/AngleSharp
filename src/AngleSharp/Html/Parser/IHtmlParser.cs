namespace AngleSharp.Html.Parser
{
    using AngleSharp.Common;
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
        IHtmlDocument Parse(String source);

        /// <summary>
        /// Parses the stream and returns the result.
        /// </summary>
        IHtmlDocument Parse(Stream source);

        /// <summary>
        /// Parses the string and returns the result.
        /// </summary>
        INodeList ParseFragment(String source, IElement contextElement);

        /// <summary>
        /// Parses the string asynchronously.
        /// </summary>
        Task<IHtmlDocument> ParseAsync(String source, CancellationToken cancel);

        /// <summary>
        /// Parses the stream asynchronously.
        /// </summary>
        Task<IHtmlDocument> ParseAsync(Stream source, CancellationToken cancel);

        /// <summary>
        /// Populates the given HTML document asynchronously.
        /// </summary>
        Task<IDocument> ParseAsync(IDocument document, CancellationToken cancel);
    }
}
