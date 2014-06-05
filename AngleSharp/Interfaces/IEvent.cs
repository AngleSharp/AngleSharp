namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// The Event interface is used to provide contextual information about
    /// an event to the handler processing the event.
    /// </summary>
    [DOM("Event")]
    public interface IEvent
    {
        /// <summary>
        /// Gets the type of event.
        /// </summary>
        [DOM("type")]
        String Type { get; }

        /// <summary>
        /// Gets the original target of the event.
        /// </summary>
        [DOM("target")]
        IEventTarget OriginalTarget { get; }

        /// <summary>
        /// Gets the current target (if bubbled).
        /// </summary>
        [DOM("currentTarget")]
        IEventTarget CurrentTarget { get; }

        /// <summary>
        /// Gets the phase of the event.
        /// </summary>
        [DOM("eventPhase")]
        EventPhase Phase { get; }

        /// <summary>
        /// Gets if the event is actually bubbling.
        /// </summary>
        [DOM("bubbles")]
        Boolean Bubbles { get; }

        /// <summary>
        /// Gets if the event is cancelable.
        /// </summary>
        [DOM("cancelable")]
        Boolean Cancelable { get; }

        /// <summary>
        /// Gets if the default behavior has been prevented.
        /// </summary>
        [DOM("defaultPrevented")]
        Boolean DefaultPrevented { get; }

        /// <summary>
        /// Gets if the event is trusted.
        /// </summary>
        [DOM("isTrusted")]
        Boolean IsTrusted { get; }

        /// <summary>
        /// Gets the originating timestamp.
        /// </summary>
        [DOM("timeStamp")]
        DateTime Time { get; }

        /// <summary>
        /// Prevents further propagation of the event.
        /// </summary>
        [DOM("stopPropagation")]
        void Stop();

        /// <summary>
        /// Stops the immediate propagation.
        /// </summary>
        [DOM("stopImmediatePropagation")]
        void StopImmediately();

        /// <summary>
        /// Prevents the default behavior.
        /// </summary>
        [DOM("preventDefault")]
        void PreventDefault();

        /// <summary>
        /// Initializes the event.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        [DOM("initEvent")]
        void Init(String type, Boolean bubbles, Boolean cancelable);
    }
}
