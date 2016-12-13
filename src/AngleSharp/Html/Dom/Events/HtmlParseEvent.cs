namespace AngleSharp.Html.Dom.Events
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;

    /// <summary>
    /// The event that is published in case of starting HTML parsing.
    /// </summary>
    public class HtmlParseEvent : Event
    {
        /// <summary>
        /// Creates a new event for starting HTML parsing.
        /// </summary>
        /// <param name="document">The document to be filled.</param>
        /// <param name="completed">Determines if parsing is done.</param>
        public HtmlParseEvent(IHtmlDocument document, Boolean completed)
            : base(completed ? EventNames.Parsed : EventNames.Parsing)
        {
            Document = document;
        }

        /// <summary>
        /// Gets the document, which is to be filled.
        /// </summary>
        public IHtmlDocument Document
        {
            get;
            private set;
        }
    }
}
