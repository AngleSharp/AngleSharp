namespace AngleSharp.Events
{
    using AngleSharp.Dom;
    using System;

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
