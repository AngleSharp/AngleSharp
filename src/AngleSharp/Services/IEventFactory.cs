namespace AngleSharp.Services
{
    using AngleSharp.Dom.Events;
    using System;

    /// <summary>
    /// Represents a factory to create event data.
    /// </summary>
    public interface IEventFactory
    {
        /// <summary>
        /// Creates a new event data object for the given event.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <returns>The event data for the given event.</returns>
        Event Create(String name);
    }
}
