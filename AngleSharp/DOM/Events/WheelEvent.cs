namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the event args for a mouse wheel event.
    /// </summary>
    [DomName("WheelEvent")]
    public class WheelEvent : MouseEvent
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public WheelEvent()
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="detail">Sets the detail id for the UI event.</param>
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
        public WheelEvent(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail, Int32 screenX, Int32 screenY, Int32 clientX, Int32 clientY, MouseButton button, IEventTarget target, String modifiersList, Double deltaX, Double deltaY, Double deltaZ, WheelMode deltaMode)
        {
            Init(type, bubbles, cancelable, view, detail, screenX, screenY, clientX, clientY, button, target, modifiersList, deltaX, deltaY, deltaZ, deltaMode);
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="eventInitDict">
        /// An optional dictionary with optional keys such as
        /// bubbles (boolean) and cancelable (boolean).
        /// </param>
        [DomConstructor]
        public WheelEvent(String type, IDictionary<String, Object> eventInitDict = null)
            : base(type, eventInitDict)
        {
            DeltaX = eventInitDict.TryGet<Double>("deltaX") ?? 0.0;
            DeltaY = eventInitDict.TryGet<Double>("deltaY") ?? 0.0;
            DeltaZ = eventInitDict.TryGet<Double>("deltaZ") ?? 0.0;
            DeltaMode = (WheelMode)(eventInitDict.TryGet<Int32>("deltaMode") ?? 0);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the mouse wheel delta X.
        /// </summary>
        [DomName("deltaX")]
        public Double DeltaX
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the mouse wheel delta Y.
        /// </summary>
        [DomName("deltaY")]
        public Double DeltaY
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the mouse wheel delta Z.
        /// </summary>
        [DomName("deltaZ")]
        public Double DeltaZ
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the mouse wheel delta mode.
        /// </summary>
        [DomName("deltaMode")]
        public WheelMode DeltaMode
        {
            get;
            private set;
        }

        #endregion

        #region Methods

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
        public void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail, Int32 screenX, Int32 screenY, Int32 clientX, Int32 clientY, MouseButton button, IEventTarget target, String modifiersList, Double deltaX, Double deltaY, Double deltaZ, WheelMode deltaMode)
        {
            Init(type, bubbles, cancelable, view, detail, screenX, screenY, clientX, clientY, 
                modifiersList.IsCtrlPressed(), modifiersList.IsAltPressed(), modifiersList.IsShiftPressed(), modifiersList.IsMetaPressed(), button, target);
            DeltaX = deltaX;
            DeltaY = deltaY;
            DeltaZ = deltaZ;
            DeltaMode = deltaMode;
        }

        #endregion
    }
}
