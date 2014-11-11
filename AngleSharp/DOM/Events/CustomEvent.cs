namespace AngleSharp.DOM.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a custom event that provides an additional details property.
    /// </summary>
    [DomName("CustomEvent")]
    public class CustomEvent : Event
    {
        #region Properties

        /// <summary>
        /// Gets the details that have been associated with the custom event.
        /// </summary>
        [DomName("detail")]
        public Object Details
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the custom event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="details">Sets the details for the custom event.</param>
        [DomName("initCustomEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable, Object details)
        {
            Init(type, bubbles, cancelable);
            Details = details;
        }

        #endregion
    }
}
