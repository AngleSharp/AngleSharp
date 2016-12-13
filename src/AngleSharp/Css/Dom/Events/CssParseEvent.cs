namespace AngleSharp.Css.Dom.Events
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;

    /// <summary>
    /// The event that is published in case of starting CSS parsing.
    /// </summary>
    public class CssParseEvent : Event
    {
        /// <summary>
        /// Creates a new event for starting CSS parsing.
        /// </summary>
        /// <param name="styleSheet">The sheet to be filled.</param>
        /// <param name="completed">Determines if parsing is done.</param>
        public CssParseEvent(ICssStyleSheet styleSheet, Boolean completed)
            : base(completed ? EventNames.Parsed : EventNames.Parsing)
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
    }
}
