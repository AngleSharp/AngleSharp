namespace AngleSharp.Events
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;

    /// <summary>
    /// The event that is published in case of starting HTML parsing.
    /// This is also the same event for SVG or XML documents.
    /// </summary>
    public class HtmlParseStartEvent : Event
    {
        /// <summary>
        /// Creates a new event for starting HTML parsing.
        /// </summary>
        /// <param name="document">The document to be filled.</param>
        public HtmlParseStartEvent(IDocument document)
        {
            Document = document;
        }

        /// <summary>
        /// Gets the document, which is to be filled.
        /// </summary>
        public IDocument Document
        {
            get;
            private set;
        }
    }
}
