namespace AngleSharp.Xml.Parser
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Xml.Dom;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the interface of an XML parser.
    /// </summary>
    public interface IXmlParser : IParser
    {
        /// <summary>
        /// Parses the string and returns the result.
        /// </summary>
        IXmlDocument Parse(String source);

        /// <summary>
        /// Parses the stream and returns the result.
        /// </summary>
        IXmlDocument Parse(Stream source);
        
        /// <summary>
        /// Parses the string asynchronously with option to cancel.
        /// </summary>
        Task<IXmlDocument> ParseAsync(String source, CancellationToken cancel);

        /// <summary>
        /// Parses the stream asynchronously with option to cancel.
        /// </summary>
        Task<IXmlDocument> ParseAsync(Stream source, CancellationToken cancel);

        /// <summary>
        /// Populates the given document asynchronously.
        /// </summary>
        Task<IDocument> ParseAsync(IDocument document, CancellationToken cancel);
    }
}
