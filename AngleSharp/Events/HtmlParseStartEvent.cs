namespace AngleSharp.Events
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The event that is published in case of starting HTML parsing.
    /// This is also the same event for SVG or XML documents.
    /// </summary>
    public class HtmlParseStartEvent
    {
        /// <summary>
        /// Action called once the parsing ended.
        /// </summary>
        public event EventHandler Ended;

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

        internal void FireEnd()
        {
            if (Ended != null)
            {
                Ended(this, EventArgs.Empty);
                Ended = null;
            }
        }
    }
}
