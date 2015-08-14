namespace AngleSharp.Events.Default
{
    using System;

    /// <summary>
    /// Bridge between non-generic IEventAggregator and generic ISubscriber, 
    /// wrapped in a specialization of this interface.
    /// </summary>
    interface IEventBroker
    {
        /// <summary>
        /// Broker accepts the generic parameter.
        /// </summary>
        /// <typeparam name="TEvent">Type of event.</typeparam>
        /// <returns>True if TEvent == T of subscriber.</returns>
        Boolean Accepts<TEvent>();

        /// <summary>
        /// Contains a reference to the same listener.
        /// </summary>
        /// <param name="listener">The listener reference.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        Boolean Contains(Object listener);

        /// <summary>
        /// Distributes the given event data.
        /// </summary>
        /// <param name="data">The data to distribute.</param>
        void Distribute(Object data);
    }
}
