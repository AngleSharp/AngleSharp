namespace AngleSharp.Events
{
    /// <summary>
    /// Declares the interface for an event listener.
    /// </summary>
    /// <typeparam name="TEvent">The type of event.</typeparam>
    public interface ISubscriber<TEvent>
    {
        /// <summary>
        /// Called once event data has been published.
        /// </summary>
        /// <param name="data">The event's data.</param>
        void OnEventData(TEvent data);
    }
}
