namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Events;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of extensions for EventTarget objects.
    /// </summary>
    public static class EventTargetExtensions
    {
        /// <summary>
        /// Firing a simple event means that a trusted event with a name, which
        /// does not bubble, is not cancelable and which uses the Event
        /// interface. It is created and dispatched at the given target.
        /// </summary>
        /// <param name="target">The target of the simple event.</param>
        /// <param name="eventName">The name of the event to be fired.</param>
        /// <param name="bubble">Optional parameter to enable bubbling.</param>
        /// <param name="cancelable">Should it be cancelable?</param>
        /// <returns>
        /// True if the element was cancelled, otherwise false.
        /// </returns>
        public static Boolean FireSimpleEvent(this IEventTarget target, String eventName, Boolean bubble = false, Boolean cancelable = false)
        {
            var eventData = new Event { IsTrusted = true };
            eventData.Init(eventName, bubble, cancelable);
            return eventData.Dispatch(target);
        }

        /// <summary>
        /// Fires a trusted event with the provided event data.
        /// </summary>
        /// <param name="target">The target of the event.</param>
        /// <param name="eventData">The event data to dispatch.</param>
        /// <returns>
        /// True if the element was cancelled, otherwise false.
        /// </returns>
        public static Boolean Fire(this IEventTarget target, Event eventData)
        {
            eventData.IsTrusted = true;
            return eventData.Dispatch(target);
        }

        /// <summary>
        /// Firing an event means dispatching the initialized (and trusted) 
        /// event at the specified event target.
        /// </summary>
        /// <param name="target">
        /// The target, where the event has been invoked.
        /// </param>
        /// <param name="initializer">The used initializer.</param>
        /// <param name="targetOverride">
        /// The current event target, if different to the invoked target.
        /// </param>
        /// <returns>
        /// True if the element was cancelled, otherwise false.
        /// </returns>
        public static Boolean Fire<T>(this IEventTarget target, Action<T> initializer, IEventTarget targetOverride = null)
            where T : Event, new()
        {
            var eventData = new T { IsTrusted = true };
            initializer(eventData);
            return eventData.Dispatch(targetOverride ?? target);
        }

        /// <summary>
        /// Returns a task that is completed once the event is fired.
        /// </summary>
        /// <typeparam name="TEventTarget">The event target type.</typeparam>
        /// <param name="node">The node that fires the event.</param>
        /// <param name="eventName">The name of the event to be awaited.</param>
        /// <returns>The awaitable task returning the event arguments.</returns>
        public static async Task<Event> AwaitEventAsync<TEventTarget>(this TEventTarget node, String eventName)
            where TEventTarget : IEventTarget
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (eventName == null)
                throw new ArgumentNullException(nameof(eventName));

            var completion = new TaskCompletionSource<Event>();
            void handler(Object s, Event ev) => completion.TrySetResult(ev);
            node.AddEventListener(eventName, handler);

            try
            {
                return await completion.Task.ConfigureAwait(false);
            }
            finally
            {
                node.RemoveEventListener(eventName, handler);
            }
        }
    }
}
