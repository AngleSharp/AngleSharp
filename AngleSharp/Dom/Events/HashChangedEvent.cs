namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a custom event that provides an additional details property.
    /// </summary>
    [DomName("HashChangeEvent")]
    public class HashChangedEvent : Event
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public HashChangedEvent()
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="previousUrl">The previous URL.</param>
        /// <param name="currentUrl">The current URL.</param>
        public HashChangedEvent(String type, Boolean bubbles, Boolean cancelable, String previousUrl, String currentUrl)
        {
            Init(type, bubbles, cancelable, previousUrl, currentUrl);
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
        public HashChangedEvent(String type, IDictionary<String, Object> eventInitDict = null)
            : base(type, eventInitDict)
        {
            PreviousUrl = (eventInitDict.TryGet("oldURL") ?? String.Empty).ToString();
            CurrentUrl = (eventInitDict.TryGet("newURL") ?? String.Empty).ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the URL before the hash changed.
        /// </summary>
        [DomName("oldURL")]
        public String PreviousUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the URL after the hash changed.
        /// </summary>
        [DomName("newURL")]
        public String CurrentUrl
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the hashchanged event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="previousUrl">The previous URL.</param>
        /// <param name="currentUrl">The current URL.</param>
        [DomName("initHashChangedEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable, String previousUrl, String currentUrl)
        {
            Init(type, bubbles, cancelable);
            Stop();
            PreviousUrl = previousUrl;
            CurrentUrl = currentUrl;
        }

        #endregion
    }
}
