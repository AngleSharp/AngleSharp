namespace AngleSharp.Events
{
    /// <summary>
    /// Represents the central event aggregation unit. Used for state events.
    /// Can be utilized for performance monitoring and more.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Publishes the given event data to all listeners.
        /// </summary>
        /// <typeparam name="TEvent">The type of event data.</typeparam>
        /// <param name="data">The carried event data.</param>
        void Publish<TEvent>(TEvent data);
    }
}
