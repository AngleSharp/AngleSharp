namespace AngleSharp.Extensions
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Events;
    using System;

    /// <summary>
    /// A set of extensions for EventTarget objects.
    /// </summary>
    static class EventTargetExtensions
    {
        /// <summary>
        /// Firing a simple event named e means that a trusted event with the name e,
        /// which does not bubble (except where otherwise stated) and is not cancelable
        /// (except where otherwise stated), and which uses the Event interface, must
        /// be created and dispatched at the given target.
        /// </summary>
        /// <param name="target">The target of the simple event.</param>
        /// <param name="eventName">The name of the event to be fired.</param>
        /// <param name="bubble">Optional parameter to enable bubbling.</param>
        /// <param name="cancelable">Optional parameter to make it cancelable.</param>
        /// <returns>True if the element was cancelled, otherwise false.</returns>
        public static Boolean FireSimpleEvent(this EventTarget target, String eventName, Boolean bubble = false, Boolean cancelable = false)
        {
            var ev = new Event { IsTrusted = true };
            ev.Init(eventName, bubble, cancelable);
            return ev.Dispatch(target);
        }

        /// <summary>
        /// Firing an event means dispatching the initialized (and trusted) event
        /// at the specified event target.
        /// </summary>
        /// <param name="target">The target of the event.</param>
        /// <param name="initializer">The used initializer.</param>
        /// <param name="currentTarget">The current event target, if different to the target.</param>
        /// <returns>True if the element was cancelled, otherwise false.</returns>
        public static Boolean Fire<T>(this EventTarget target, Action<T> initializer, IEventTarget currentTarget = null)
            where T : Event, new()
        {
            var ev = new T { IsTrusted = true };
            initializer(ev);
            //TODO dispatch at currentTarget for target (!)
            return ev.Dispatch(target);
        }
    }
}
