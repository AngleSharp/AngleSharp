namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents a custom event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DomName("CustomEvent")]
    public interface ICustomEvent<T> : IEvent
    {
        /// <summary>
        /// Gets the details that have been associated with the custom event.
        /// </summary>
        [DomName("detail")]
        T Details { get; }

        /// <summary>
        /// Initializes the custom event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="details">Sets the details for the custom event.</param>
        [DomName("initCustomEvent")]
        void Init(String type, Boolean bubbles, Boolean cancelable, T details);
    }
}
