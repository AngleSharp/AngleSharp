namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;

    /// <summary>
    /// A set of extensions for EventTarget objects.
    /// </summary>
    static class EventTargetExtensions
    {
        /// <summary>
        /// Firing a simple event named e means that a trusted event with a name, which
        /// does not bubble, is not cancelable and which uses the Event interface. It is
        /// created and dispatched at the given target.
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
        /// Firing an event means dispatching the initialized (and trusted) event at the specified event target.
        /// </summary>
        /// <param name="target">The target, where the event has been invoked.</param>
        /// <param name="initializer">The used initializer.</param>
        /// <param name="targetOverride">The current event target, if different to the invoked target.</param>
        /// <returns>True if the element was cancelled, otherwise false.</returns>
        public static Boolean Fire<T>(this EventTarget target, Action<T> initializer, EventTarget targetOverride = null)
            where T : Event, new()
        {
            var ev = new T { IsTrusted = true };
            initializer(ev);
            return ev.Dispatch(targetOverride ?? target);
        }
    }
}
