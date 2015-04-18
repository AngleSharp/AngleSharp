namespace AngleSharp.Events
{
    using System;
    using AngleSharp.Dom;
    using AngleSharp.Parser.Html;

    /// <summary>
    /// The event that is published in case of starting HTML parsing.
    /// </summary>
    public class HtmlParseStartEvent
    {
        /// <summary>
        /// Action called once the parsing ended.
        /// </summary>
        public event Action<IDocument> Ended;

        /// <summary>
        /// Creates a new event for parsing a new HTML document.
        /// </summary>
        /// <param name="parser">The associated parser.</param>
        public HtmlParseStartEvent(HtmlParser parser)
        {
            Parser = parser;
        }

        /// <summary>
        /// Gets the associated parser.
        /// </summary>
        public HtmlParser Parser
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the document by invoking the ended event.
        /// </summary>
        /// <param name="document">The document to propagate.</param>
        public void SetResult(IDocument document)
        {
            if (Ended != null)
                Ended(document);
        }
    }
}
