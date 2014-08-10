namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents the event arguments for a mouse wheel event.
    /// </summary>
    [DomName("WheelEvent")]
    public interface IWheelEvent : IEvent
    {
        /// <summary>
        /// Gets the mouse wheel delta X.
        /// </summary>
        [DomName("deltaX")]
        Double DeltaX { get; }

        /// <summary>
        /// Gets the mouse wheel delta Y.
        /// </summary>
        [DomName("deltaY")]
        Double DeltaY { get; }

        /// <summary>
        /// Gets the mouse wheel delta Z.
        /// </summary>
        [DomName("deltaZ")]
        Double DeltaZ { get; }

        /// <summary>
        /// Gets the mouse wheel delta mode.
        /// </summary>
        [DomName("deltaMode")]
        WheelMode DeltaMode { get; }

        /// <summary>
        /// Initializes the mouse wheel event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="detail">Sets the detail id for the UIevent.</param>
        /// <param name="screenX">Sets the screen X coordinate.</param>
        /// <param name="screenY">Sets the screen Y coordinate.</param>
        /// <param name="clientX">Sets the client X coordinate.</param>
        /// <param name="clientY">Sets the client Y coordinate.</param>
        /// <param name="button">Sets which button has been pressed.</param>
        /// <param name="target">The target of the mouse event.</param>
        /// <param name="modifiersList">A list with keyboard modifiers that have been pressed.</param>
        /// <param name="deltaX">The mouse wheel delta in X direction.</param>
        /// <param name="deltaY">The mouse wheel delta in Y direction.</param>
        /// <param name="deltaZ">The mouse wheel delta in Z direction.</param>
        /// <param name="deltaMode">The delta mode for the wheel event.</param>
        [DomName("initWheelEvent")]
        void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail, 
            Int32 screenX, Int32 screenY, Int32 clientX, Int32 clientY, MouseButton button, IEventTarget target, 
            String modifiersList, Double deltaX, Double deltaY, Double deltaZ, WheelMode deltaMode);
    }
}
