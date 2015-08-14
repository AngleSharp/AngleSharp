namespace AngleSharp.Events.Default
{
    using System;

    /// <summary>
    /// Wrapper for the generic ISubscriber.
    /// </summary>
    sealed class EventBroker<TContract> : IEventBroker
    {
        readonly ISubscriber<TContract> _listener;

        /// <summary>
        /// Creates a new event broker.
        /// </summary>
        /// <param name="listener">The subscriber to wrap.</param>
        public EventBroker(ISubscriber<TContract> listener)
        {
            _listener = listener;
        }

        /// <summary>
        /// Checks if the kind of message is the contract.
        /// </summary>
        /// <typeparam name="TMessage">Type of event.</typeparam>
        /// <returns>True if TMessage == TContract.</returns>
        public Boolean Accepts<TMessage>()
        {
            return typeof(TMessage) == typeof(TContract);
        }

        /// <summary>
        /// Contains a reference to the same listener.
        /// </summary>
        /// <param name="listener">The listener reference.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Contains(Object listener)
        {
            return Object.ReferenceEquals(_listener, listener);
        }

        /// <summary>
        /// Distributes the given event data.
        /// </summary>
        /// <param name="data">The data to distribute.</param>
        public void Distribute(Object data)
        {
            _listener.OnEventData((TContract)data);
        }
    }
}
