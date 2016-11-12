namespace AngleSharp.Xml.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Xml.Dom;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extensions for the IXmlParser instances.
    /// </summary>
    public static class XmlParserExtensions
    {
        /// <summary>
        /// Parses the string asynchronously.
        /// </summary>
        public static Task<IXmlDocument> ParseDocumentAsync(this IXmlParser parser, String source)
        {
            return parser.ParseDocumentAsync(source, CancellationToken.None);
        }

        /// <summary>
        /// Parses the stream asynchronously.
        /// </summary>
        public static Task<IXmlDocument> ParseDocumentAsync(this IXmlParser parser, Stream source)
        {
            return parser.ParseDocumentAsync(source, CancellationToken.None);
        }

        /// <summary>
        /// Populates the given document asynchronously.
        /// </summary>
        public static Task<IDocument> ParseDocumentAsync(this IXmlParser parser, IDocument document)
        {
            return parser.ParseDocumentAsync(document, CancellationToken.None);
        }
    }
}
