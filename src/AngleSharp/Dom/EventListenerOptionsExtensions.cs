namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The members of event targets taking options instead of just if they capture.
    /// </summary>
    [DomExposed("EventTarget")]
    public static class EventListenerOptionsExtensions
    {
        /// <summary>
        /// Registers an event listener in the given way.
        /// </summary>
        /// <param name="target">The target of the events.</param>
        /// <param name="type">The type of the events.</param>
        /// <param name="callback">The listener.</param>
        /// <param name="options">How the listener is registered.</param>
        [DomName("addEventListener")]
        public static void AddEventListener(this IEventTarget target, String type, DomEventHandler? callback, AddEventListenerOptions? options)
        {
            if (target is EventTarget eventTarget)
            {
                eventTarget.AddEventListener(type, callback, options);
            }
            else
            {
                target.AddEventListener(type, callback, options?.Capture ?? false);
            }
        }

        /// <summary>
        /// Removes an event listener that was registered in the given way.
        /// </summary>
        /// <param name="target">The target of the events.</param>
        /// <param name="type">The type of the events.</param>
        /// <param name="callback">The listener.</param>
        /// <param name="options">How the listener was registered.</param>
        [DomName("removeEventListener")]
        public static void RemoveEventListener(this IEventTarget target, String type, DomEventHandler? callback, EventListenerOptions? options) =>
            target.RemoveEventListener(type, callback, options?.Capture ?? false);
    }
}
