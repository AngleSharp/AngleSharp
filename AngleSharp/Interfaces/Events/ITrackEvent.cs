namespace AngleSharp.DOM.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the event arguments for a track event.
    /// </summary>
    [DomName("TrackEvent")]
    public interface ITrackEvent : IEvent
    {
        /// <summary>
        /// Gets the assigned track object, if any.
        /// </summary>
        [DomName("track")]
        Object Track { get; }
    }
}
