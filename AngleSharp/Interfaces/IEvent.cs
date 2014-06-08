namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// The Event interface is used to provide contextual information about
    /// an event to the handler processing the event.
    /// </summary>
    [DomName("Event")]
    public interface IEvent
    {
        /// <summary>
        /// Gets the type of event.
        /// </summary>
        [DomName("type")]
        String Type { get; }

        /// <summary>
        /// Gets the original target of the event.
        /// </summary>
        [DomName("target")]
        IEventTarget OriginalTarget { get; }

        /// <summary>
        /// Gets the current target (if bubbled).
        /// </summary>
        [DomName("currentTarget")]
        IEventTarget CurrentTarget { get; }

        /// <summary>
        /// Gets the phase of the event.
        /// </summary>
        [DomName("eventPhase")]
        EventPhase Phase { get; }

        /// <summary>
        /// Gets if the event is actually bubbling.
        /// </summary>
        [DomName("bubbles")]
        Boolean Bubbles { get; }

        /// <summary>
        /// Gets if the event is cancelable.
        /// </summary>
        [DomName("cancelable")]
        Boolean Cancelable { get; }

        /// <summary>
        /// Gets if the default behavior has been prevented.
        /// </summary>
        [DomName("defaultPrevented")]
        Boolean DefaultPrevented { get; }

        /// <summary>
        /// Gets if the event is trusted.
        /// </summary>
        [DomName("isTrusted")]
        Boolean IsTrusted { get; }

        /// <summary>
        /// Gets the originating timestamp.
        /// </summary>
        [DomName("timeStamp")]
        DateTime Time { get; }

        /// <summary>
        /// Prevents further propagation of the event.
        /// </summary>
        [DomName("stopPropagation")]
        void Stop();

        /// <summary>
        /// Stops the immediate propagation.
        /// </summary>
        [DomName("stopImmediatePropagation")]
        void StopImmediately();

        /// <summary>
        /// Prevents the default behavior.
        /// </summary>
        [DomName("preventDefault")]
        void PreventDefault();

        /// <summary>
        /// Initializes the event.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        [DomName("initEvent")]
        void Init(String type, Boolean bubbles, Boolean cancelable);
    }
}
