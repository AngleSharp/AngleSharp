namespace AngleSharp.Events
{
    using AngleSharp.Dom.Css;
    using System;

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
