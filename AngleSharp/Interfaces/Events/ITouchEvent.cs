namespace AngleSharp.DOM.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the event arguments for a touch event.
    /// </summary>
    [DomName("TouchEvent")]
    public interface ITouchEvent : IUiEvent
    {
        /// <summary>
        /// Gets a list with all active touch points.
        /// </summary>
        [DomName("touches")]
        ITouchList Touches { get; }

        /// <summary>
        /// Gets a list with touch points over the target.
        /// </summary>
        [DomName("targetTouches")]
        ITouchList TargetTouches { get; }

        /// <summary>
        /// Gets a list with changed touch points.
        /// </summary>
        [DomName("changedTouches")]
        ITouchList ChangedTouches { get; }

        /// <summary>
        /// Gets if the alt key is pressed.
        /// </summary>
        [DomName("altKey")]
        Boolean IsAltPressed { get; }

        /// <summary>
        /// Gets if the meta key is pressed.
        /// </summary>
        [DomName("metaKey")]
        Boolean IsMetaPressed { get; }

        /// <summary>
        /// Gets if the control key is pressed.
        /// </summary>
        [DomName("ctrlKey")]
        Boolean IsCtrlPressed { get; }

        /// <summary>
        /// Gets if the shift key is pressed.
        /// </summary>
        [DomName("shiftKey")]
        Boolean IsShiftPressed { get; }
    }
}
