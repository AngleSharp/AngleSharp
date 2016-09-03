namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of extensions for EventTarget objects.
    /// </summary>
    static class EventTargetExtensions
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="eventName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Task FireAsync<T>(this IBrowsingContext target, String eventName, T data)
        {
            var ev = new InteractivityEvent<T>(eventName, data);
            target.Fire(ev);
            return ev.Result ?? TaskEx.FromResult(false);
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
    }
}
