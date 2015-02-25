namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a custom event that provides an additional details property.
    /// </summary>
    [DomName("CustomEvent")]
    public class CustomEvent : Event
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public CustomEvent()
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="details">Sets the details for the custom event.</param>
        public CustomEvent(String type, Boolean bubbles, Boolean cancelable, Object details)
        {
            Init(type, bubbles, cancelable, details);
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="eventInitDict">
        /// An optional dictionary with optional keys such as
        /// bubbles (boolean) and cancelable (boolean).
        /// </param>
        [DomConstructor]
        public CustomEvent(String type, IDictionary<String, Object> eventInitDict = null)
            : base(type, eventInitDict)
        {
            Details = eventInitDict.TryGet("detail");
        }

        #endregion

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
