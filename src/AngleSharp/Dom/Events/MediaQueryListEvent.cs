namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the media query list event arguments.
    /// </summary>
    [DomName("MediaQueryListEvent")]
    public class MediaQueryListEvent : Event
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public MediaQueryListEvent()
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="media">Sets the associated media string.</param>
        /// <param name="matches">Sets the matches flag.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public MediaQueryListEvent(String type, Boolean bubbles = false, Boolean cancelable = false, String media = null, Boolean matches = false)
        {
            Init(type, bubbles, cancelable, media, matches);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the current media matches.
        /// </summary>
        [DomName("matches")]
        public Boolean IsMatch
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current media string.
        /// </summary>
        [DomName("media")]
        public String Media
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the media query list event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="media">Sets the associated media string.</param>
        /// <param name="matches">Sets the matches flag.</param>
        [DomName("initMediaQueryListEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable, String media, Boolean matches)
        {
            Init(type, bubbles, cancelable);
            Media = media;
            IsMatch = matches;
        }

        #endregion
    }
}
