namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// How an event listener is registered.
    /// </summary>
    [DomName("AddEventListenerOptions")]
    public sealed class AddEventListenerOptions
    {
        /// <summary>
        /// Gets or sets if the listener is called while the event is captured.
        /// </summary>
        [DomName("capture")]
        public Boolean Capture { get; set; }

        /// <summary>
        /// Gets or sets if the listener is removed when it is called.
        /// </summary>
        [DomName("once")]
        public Boolean Once { get; set; }

        /// <summary>
        /// Gets or sets if the listener will not prevent the default of the event, which is ignored
        /// if it tries.
        /// </summary>
        [DomName("passive")]
        public Boolean Passive { get; set; }

        /// <summary>
        /// Gets or sets the signal that removes the listener.
        /// </summary>
        [DomName("signal")]
        public IAbortSignal? Signal { get; set; }
    }
}
