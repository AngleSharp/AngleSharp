namespace AngleSharp.Dom.Events
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The event that is published in case of starting HTML parsing.
    /// This is also the same event for SVG or XML documents.
    /// </summary>
    public class HtmlParseEvent : Event
    {
        /// <summary>
        /// Creates a new event for starting HTML parsing.
        /// </summary>
        /// <param name="document">The document to be filled.</param>
        /// <param name="completed">Determines if parsing is done.</param>
        public HtmlParseEvent(IDocument document, Boolean completed)
            : base(completed ? EventNames.ParseEnd : EventNames.ParseStart)
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
