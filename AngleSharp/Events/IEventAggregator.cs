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

        /// <summary>
        /// Subscribes to receive all published events of the given type.
        /// Stores a weak reference to the listener.
        /// </summary>
        /// <typeparam name="TEvent">The type of events to receive.</typeparam>
        /// <param name="listener">The receiver of the event data.</param>
        void Subscribe<TEvent>(ISubscriber<TEvent> listener);

        /// <summary>
        /// Unsubscribes to stop receiving the published events. This is not
        /// always required, since weak references to the listener will not
        /// infer with object disposal.
        /// </summary>
        /// <typeparam name="TEvent">The type of events to block.</typeparam>
        /// <param name="listener">The receiver that is </param>
        void Unsubscribe<TEvent>(ISubscriber<TEvent> listener);
    }
}
