namespace AngleSharp.Html.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the event of a pointing device, e.g. a mouse, a pen or a touch.
    /// </summary>
    [DomName("PointerEvent")]
    [DomExposed("Window")]
    public class PointerEvent : MouseEvent
    {
        /// <summary>
        /// Creates a new event with the members of a PointerEventInit.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="detail">Sets the detail id for the UIEvent.</param>
        /// <param name="screenX">Sets the screen X coordinate.</param>
        /// <param name="screenY">Sets the screen Y coordinate.</param>
        /// <param name="clientX">Sets the client X coordinate.</param>
        /// <param name="clientY">Sets the client Y coordinate.</param>
        /// <param name="ctrlKey">Sets if the control key is pressed.</param>
        /// <param name="altKey">Sets if the alt key is pressed.</param>
        /// <param name="shiftKey">Sets if the shift key is pressed.</param>
        /// <param name="metaKey">Sets if the meta key is pressed.</param>
        /// <param name="button">Sets which button has been pressed.</param>
        /// <param name="buttons">Sets the buttons being held.</param>
        /// <param name="relatedTarget">The target of the mouse event.</param>
        /// <param name="pointerId">Sets the identifier of the pointer.</param>
        /// <param name="width">Sets the width of the contact.</param>
        /// <param name="height">Sets the height of the contact.</param>
        /// <param name="pressure">Sets the pressure of the contact.</param>
        /// <param name="tangentialPressure">Sets the tangential pressure of the contact.</param>
        /// <param name="tiltX">Sets the tilt of a pen in X direction.</param>
        /// <param name="tiltY">Sets the tilt of a pen in Y direction.</param>
        /// <param name="twist">Sets the rotation of a pen.</param>
        /// <param name="pointerType">Sets the type of the device, e.g. mouse.</param>
        /// <param name="isPrimary">Sets if it is the primary pointer.</param>
        /// <param name="movementX">Sets the horizontal distance moved since the previous mouse event.</param>
        /// <param name="movementY">Sets the vertical distance moved since the previous mouse event.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public PointerEvent(String type, Boolean bubbles = false, Boolean cancelable = false, IWindow? view = null, Int32 detail = 0, Int32 screenX = 0, Int32 screenY = 0, Int32 clientX = 0, Int32 clientY = 0, Boolean ctrlKey = false, Boolean altKey = false, Boolean shiftKey = false, Boolean metaKey = false, MouseButton button = MouseButton.Primary, MouseButtons buttons = default, IEventTarget? relatedTarget = null, Int32 pointerId = 0, Double width = 1.0, Double height = 1.0, Double pressure = 0.0, Double tangentialPressure = 0.0, Int32 tiltX = 0, Int32 tiltY = 0, Int32 twist = 0, String? pointerType = null, Boolean isPrimary = false, Int32 movementX = 0, Int32 movementY = 0)
            : base(type, bubbles, cancelable, view, detail, screenX, screenY, clientX, clientY, ctrlKey, altKey, shiftKey, metaKey, button, relatedTarget, movementX, movementY)
        {
            Buttons = buttons;
            PointerId = pointerId;
            Width = width;
            Height = height;
            Pressure = pressure;
            TangentialPressure = tangentialPressure;
            TiltX = tiltX;
            TiltY = tiltY;
            Twist = twist;
            PointerType = pointerType ?? String.Empty;
            IsPrimary = isPrimary;
        }

        /// <summary>
        /// Gets the identifier of the pointer.
        /// </summary>
        [DomName("pointerId")]
        public Int32 PointerId { get; }

        /// <summary>
        /// Gets the width of the contact.
        /// </summary>
        [DomName("width")]
        public Double Width { get; }

        /// <summary>
        /// Gets the height of the contact.
        /// </summary>
        [DomName("height")]
        public Double Height { get; }

        /// <summary>
        /// Gets the pressure of the contact.
        /// </summary>
        [DomName("pressure")]
        public Double Pressure { get; }

        /// <summary>
        /// Gets the tangential pressure of the contact.
        /// </summary>
        [DomName("tangentialPressure")]
        public Double TangentialPressure { get; }

        /// <summary>
        /// Gets the tilt of a pen in X direction.
        /// </summary>
        [DomName("tiltX")]
        public Int32 TiltX { get; }

        /// <summary>
        /// Gets the tilt of a pen in Y direction.
        /// </summary>
        [DomName("tiltY")]
        public Int32 TiltY { get; }

        /// <summary>
        /// Gets the rotation of a pen.
        /// </summary>
        [DomName("twist")]
        public Int32 Twist { get; }

        /// <summary>
        /// Gets the type of the device, e.g. mouse, pen or touch.
        /// </summary>
        [DomName("pointerType")]
        public String PointerType { get; }

        /// <summary>
        /// Gets if it is the primary pointer.
        /// </summary>
        [DomName("isPrimary")]
        public Boolean IsPrimary { get; }
    }
}
