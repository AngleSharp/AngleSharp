namespace AngleSharp.Events
{
    using AngleSharp.Dom.Css;
    using System;

    /// <summary>
    /// The event that is published in case of starting CSS parsing.
    /// </summary>
    public class CssParseStartEvent : IDisposable
    {
        /// <summary>
        /// Action called once the parsing ended.
        /// </summary>
        public event EventHandler Ended;

        /// <summary>
        /// Creates a new event for starting CSS parsing.
        /// </summary>
        /// <param name="styleSheet">The sheet to be filled.</param>
        public CssParseStartEvent(ICssStyleSheet styleSheet)
        {
            StyleSheet = styleSheet;
        }

        /// <summary>
        /// Gets the stylesheet, which is to be filled.
        /// </summary>
        public ICssStyleSheet StyleSheet 
        { 
            get; 
            private set; 
        }

        void IDisposable.Dispose()
        {
            if (Ended != null)
            {
                Ended(this, EventArgs.Empty);
                Ended = null;
            }
        }
    }
}
