namespace AngleSharp.DOM.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a track that provides an additional track information.
    /// </summary>
    [DomName("TrackEvent")]
    public class TrackEvent : Event
    {
        #region Properties

        /// <summary>
        /// Gets the assigned track object, if any.
        /// </summary>
        [DomName("track")]
        public Object Track
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the mouse event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="track">The track object.</param>
        [DomName("initTrackEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable, Object track)
        {
            Init(type, bubbles, cancelable);
            Track = track;
        }

        #endregion
    }
}
