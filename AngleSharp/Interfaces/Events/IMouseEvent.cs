namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents the event arguments for a mouse event.
    /// </summary>
    [DomName("MouseEvent")]
    public interface IMouseEvent : IUiEvent
    {
        /// <summary>
        /// Gets the screen X coordinates.
        /// </summary>
        [DomName("screenX")]
        Int32 ScreenX { get; }

        /// <summary>
        /// Gets the screen Y coordinates.
        /// </summary>
        [DomName("screenY")]
        Int32 ScreenY { get; }

        /// <summary>
        /// Gets the client X coordinates.
        /// </summary>
        [DomName("clientX")]
        Int32 ClientX { get; }

        /// <summary>
        /// Gets the client Y coordinates.
        /// </summary>
        [DomName("clientY")]
        Int32 ClientY { get; }

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
        /// Gets which button has been pressed.
        /// </summary>
        [DomName("button")]
        MouseButton Button { get; }

        /// <summary>
        /// Gets the currently pressed buttons.
        /// </summary>
        [DomName("buttons")]
        MouseButtons Buttons { get; }

        /// <summary>
        /// Gets the target of the mouse event.
        /// </summary>
        [DomName("relatedTarget")]
        IEventTarget Target { get; }

        /// <summary>
        /// Returns the current state of the specified modifier key.
        /// </summary>
        /// <param name="key">The modifier key to lookup.</param>
        /// <returns>True if the key is currently pressed, otherwise false.</returns>
        [DomName("getModifierState")]
        Boolean GetModifierState(String key);

        /// <summary>
        /// Initializes the mouse event.
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
        /// <param name="ctrlKey">Sets if the control key was pressed.</param>
        /// <param name="altKey">Sets if the alt key was pressed.</param>
        /// <param name="shiftKey">Sets if the shift key was pressed.</param>
        /// <param name="metaKey">Sets if the meta key was pressed.</param>
        /// <param name="button">Sets which button has been pressed.</param>
        /// <param name="target">The target of the mouse event.</param>
        [DomName("initMouseEvent")]
        void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail, 
            Int32 screenX, Int32 screenY, Int32 clientX, Int32 clientY, Boolean ctrlKey, Boolean altKey, Boolean shiftKey, Boolean metaKey, MouseButton button, IEventTarget target);
    }
}
