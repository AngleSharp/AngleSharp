namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using System;

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
        /// <param name="oldURL">The previous URL.</param>
        /// <param name="newURL">The current URL.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public HashChangedEvent(String type, Boolean bubbles = false, Boolean cancelable = false, String oldURL = null, String newURL = null)
        {
            Init(type, bubbles, cancelable, oldURL ?? String.Empty, newURL ?? String.Empty);
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
