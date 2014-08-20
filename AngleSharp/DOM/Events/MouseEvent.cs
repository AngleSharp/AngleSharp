namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents the event args for a mouse event.
    /// </summary>
    class MouseEvent : UiEvent, IMouseEvent
    {
        #region Properties 

        public Int32 ScreenX
        {
            get;
            private set;
        }

        public Int32 ScreenY
        {
            get;
            private set;
        }

        public Int32 ClientX
        {
            get;
            private set;
        }

        public Int32 ClientY
        {
            get;
            private set;
        }

        public Boolean IsCtrlPressed
        {
            get;
            private set;
        }

        public Boolean IsShiftPressed
        {
            get;
            private set;
        }

        public Boolean IsAltPressed
        {
            get;
            private set;
        }

        public Boolean IsMetaPressed
        {
            get;
            private set;
        }

        public MouseButton Button
        {
            get;
            private set;
        }

        public MouseButtons Buttons
        {
            get;
            private set;
        }

        public IEventTarget Target
        {
            get;
            private set;
        }

        public Boolean GetModifierState(String key)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

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
