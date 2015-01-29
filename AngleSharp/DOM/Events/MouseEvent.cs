namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the event args for a mouse event.
    /// </summary>
    [DomName("MouseEvent")]
    public class MouseEvent : UiEvent
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public MouseEvent()
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
        /// <param name="ctrlKey">Sets if the control key was pressed.</param>
        /// <param name="altKey">Sets if the alt key was pressed.</param>
        /// <param name="shiftKey">Sets if the shift key was pressed.</param>
        /// <param name="metaKey">Sets if the meta key was pressed.</param>
        /// <param name="button">Sets which button has been pressed.</param>
        /// <param name="target">The target of the mouse event.</param>
        public MouseEvent(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail, Int32 screenX, Int32 screenY, Int32 clientX, Int32 clientY, Boolean ctrlKey, Boolean altKey, Boolean shiftKey, Boolean metaKey, MouseButton button, IEventTarget target)
        {
            Init(type, bubbles, cancelable, view, detail, screenX, screenY, clientX, clientY, ctrlKey, altKey, shiftKey, metaKey, button, target);
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
        public MouseEvent(String type, IDictionary<String, Object> eventInitDict = null)
            : base(type, eventInitDict)
        {
            ScreenX = eventInitDict.TryGet<Int32>("screenX") ?? 0;
            ScreenY = eventInitDict.TryGet<Int32>("screenY") ?? 0;
            ClientX = eventInitDict.TryGet<Int32>("clientX") ?? 0;
            ClientY = eventInitDict.TryGet<Int32>("clientY") ?? 0;
            IsCtrlPressed = eventInitDict.TryGet<Boolean>("ctrlKey") ?? false;
            IsMetaPressed = eventInitDict.TryGet<Boolean>("metaKey") ?? false;
            IsShiftPressed = eventInitDict.TryGet<Boolean>("shiftKey") ?? false;
            IsAltPressed = eventInitDict.TryGet<Boolean>("altKey") ?? false;
            Button = (MouseButton)(eventInitDict.TryGet<Int32>("button") ?? 0);
            Target = eventInitDict.TryGet("relatedTarget") as IEventTarget;
        }

        #endregion

        #region Properties 

        /// <summary>
        /// Gets the screen X coordinates.
        /// </summary>
        [DomName("screenX")]
        public Int32 ScreenX
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the screen Y coordinates.
        /// </summary>
        [DomName("screenY")]
        public Int32 ScreenY
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the client X coordinates.
        /// </summary>
        [DomName("clientX")]
        public Int32 ClientX
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the client Y coordinates.
        /// </summary>
        [DomName("clientY")]
        public Int32 ClientY
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets if the control key is pressed.
        /// </summary>
        [DomName("ctrlKey")]
        public Boolean IsCtrlPressed
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets if the shift key is pressed.
        /// </summary>
        [DomName("shiftKey")]
        public Boolean IsShiftPressed
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets if the alt key is pressed.
        /// </summary>
        [DomName("altKey")]
        public Boolean IsAltPressed
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets if the meta key is pressed.
        /// </summary>
        [DomName("metaKey")]
        public Boolean IsMetaPressed
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets which button has been pressed.
        /// </summary>
        [DomName("button")]
        public MouseButton Button
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the currently pressed buttons.
        /// </summary>
        [DomName("buttons")]
        public MouseButtons Buttons
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the target of the mouse event.
        /// </summary>
        [DomName("relatedTarget")]
        public IEventTarget Target
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the current state of the specified modifier key.
        /// </summary>
        /// <param name="key">The modifier key to lookup.</param>
        /// <returns>True if the key is currently pressed, otherwise false.</returns>
        [DomName("getModifierState")]
        public Boolean GetModifierState(String key)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

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
        public void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail, Int32 screenX, Int32 screenY, Int32 clientX, Int32 clientY, Boolean ctrlKey, Boolean altKey, Boolean shiftKey, Boolean metaKey, MouseButton button, IEventTarget target)
        {
            Init(type, bubbles, cancelable, view, detail);
            ScreenX = screenX;
            ScreenY = screenY;
            ClientX = clientX;
            ClientY = clientY;
            IsCtrlPressed = ctrlKey;
            IsMetaPressed = metaKey;
            IsShiftPressed = shiftKey;
            IsAltPressed = altKey;
            Button = button;
            Target = target;
        }

        #endregion
    }
}
