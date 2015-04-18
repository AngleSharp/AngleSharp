namespace AngleSharp.Events
{
    using System;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;

    /// <summary>
    /// The event that is published in case of starting CSS parsing.
    /// </summary>
    public class CssParseStartEvent
    {
        /// <summary>
        /// Action called once the parsing ended.
        /// </summary>
        public event Action<ICssStyleSheet> Ended;

        /// <summary>
        /// Creates a new event for parsing a new CSS stylesheet.
        /// </summary>
        /// <param name="parser">The associated parser.</param>
        public CssParseStartEvent(CssParser parser)
        {
            Parser = parser;
        }

        /// <summary>
        /// Gets the associated parser.
        /// </summary>
        public CssParser Parser
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the sheet by invoking the ended event.
        /// </summary>
        /// <param name="sheet">The sheet to propagate.</param>
        public void SetResult(ICssStyleSheet sheet)
        {
            if (Ended != null)
                Ended(sheet);
        }
    }
}
