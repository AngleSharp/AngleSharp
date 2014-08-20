namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents the event args for a mouse wheel event.
    /// </summary>
    class WheelEvent : MouseEvent, IWheelEvent
    {
        #region Properties

        public Double DeltaX
        {
            get;
            private set;
        }

        public Double DeltaY
        {
            get;
            private set;
        }

        public Double DeltaZ
        {
            get;
            private set;
        }

        public WheelMode DeltaMode
        {
            get;
            private set;
        }

        #endregion

        #region Methods

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
