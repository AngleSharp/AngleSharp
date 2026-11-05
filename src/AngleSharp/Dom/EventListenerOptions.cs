namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// How an event listener to remove was registered.
    /// </summary>
    [DomName("EventListenerOptions")]
    public sealed class EventListenerOptions
    {
        /// <summary>
        /// Gets or sets if the listener is called while the event is captured.
        /// </summary>
        [DomName("capture")]
        public Boolean Capture { get; set; }
    }
}
