namespace AngleSharp.Html
{
    using System;

    /// <summary>
    /// A set of flags for an event.
    /// </summary>
    [Flags]
    enum EventFlags
    {
        /// <summary>
        /// No flags are set.
        /// </summary>
        None = 0,
        /// <summary>
        /// Stop propagation has been requested.
        /// </summary>
        StopPropagation = 0x1,
        /// <summary>
        /// Stop immediate propagation has been requested.
        /// </summary>
        StopImmediatePropagation = 0x2,
        /// <summary>
        /// The event has been cancelled.
        /// </summary>
        Canceled = 0x4,
        /// <summary>
        /// The event has been initialized.
        /// </summary>
        Initialized = 0x8,
        /// <summary>
        /// The event has been dispatched.
        /// </summary>
        Dispatch = 0x10
    }
}
