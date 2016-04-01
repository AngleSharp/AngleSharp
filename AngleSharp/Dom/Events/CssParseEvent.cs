namespace AngleSharp.Dom.Events
{
    using AngleSharp.Dom.Css;

    /// <summary>
    /// The event that is published in case of starting CSS parsing.
    /// </summary>
    public class CssParseEvent : Event
    {
        /// <summary>
        /// Creates a new event for starting CSS parsing.
        /// </summary>
        /// <param name="styleSheet">The sheet to be filled.</param>
        public CssParseEvent(ICssStyleSheet styleSheet)
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
