namespace AngleSharp.Events.Default
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a very simple event aggregator.
    /// </summary>
    public class SimpleEventAggregator : IEventAggregator
    {
        readonly List<IEventBroker> _brokers;

        /// <summary>
        /// Creates a new event aggregator.
        /// </summary>
        public SimpleEventAggregator()
        {
            _brokers = new List<IEventBroker>();
        }

        /// <summary>
        /// Publishes the given event data to all listeners.
        /// </summary>
        /// <typeparam name="TEvent">The type of event data.</typeparam>
        /// <param name="data">The carried event data.</param>
        public virtual void Publish<TEvent>(TEvent data)
        {
            var brokers = _brokers.Where(m => m.Accepts<TEvent>()).ToArray();

            for (int i = 0; i < brokers.Length; i++)
                brokers[i].Distribute(data);
        }

        /// <summary>
        /// Subscribes to receive all published events of the given type.
        /// Stores a weak reference to the listener.
        /// </summary>
        /// <typeparam name="TEvent">The type of events to receive.</typeparam>
        /// <param name="listener">The receiver of the event data.</param>
        public virtual void Subscribe<TEvent>(ISubscriber<TEvent> listener)
        {
            _brokers.Add(new EventBroker<TEvent>(listener));
        }

        /// <summary>
        /// Unsubscribes to stop receiving the published events. This is not
        /// always required, since weak references to the listener will not
        /// infer with object disposal.
        /// </summary>
        /// <typeparam name="TEvent">The type of events to block.</typeparam>
        /// <param name="listener">The receiver that is </param>
        public virtual void Unsubscribe<TEvent>(ISubscriber<TEvent> listener)
        {
            _brokers.RemoveAll(m => m.Contains(listener));
        }
    }
}
