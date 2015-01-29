namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a track that provides an additional track information.
    /// </summary>
    [DomName("TrackEvent")]
    public class TrackEvent : Event
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public TrackEvent()
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="track">The track object.</param>
        public TrackEvent(String type, Boolean bubbles, Boolean cancelable, Object track)
        {
            Init(type, bubbles, cancelable, track);
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
        public TrackEvent(String type, IDictionary<String, Object> eventInitDict = null)
            : base(type, eventInitDict)
        {
            Track = eventInitDict.TryGet("track");
        }

        #endregion

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
