namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents a track that provides an additional track information.
    /// </summary>
    sealed class TrackEvent : Event, ITrackEvent
    {
        #region Properties

        public Object Track
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Init(String type, Boolean bubbles, Boolean cancelable, Object track)
        {
            Init(type, bubbles, cancelable);
            Track = track;
        }

        #endregion
    }
}
