namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents a track that provides an additional track information.
    /// </summary>
    sealed class TrackEvent : Event, ITrackEvent
    {
        #region Fields

        Object _track;

        #endregion

        #region Properties

        public Object Track
        {
            get { return _track; }
        }

        #endregion

        #region Methods

        public void Init(String type, Boolean bubbles, Boolean cancelable, Object track)
        {
            _track = track;
            Init(type, bubbles, cancelable);
        }

        #endregion
    }
}
