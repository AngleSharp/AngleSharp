namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the event arguments for a touch event.
    /// </summary>
    [DomName("TouchEvent")]
    public class TouchEvent : UiEvent
    {
        #region Properties

        /// <summary>
        /// Gets a list with all active touch points.
        /// </summary>
        [DomName("touches")]
        public ITouchList Touches
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a list with touch points over the target.
        /// </summary>
        [DomName("targetTouches")]
        public ITouchList TargetTouches
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a list with changed touch points.
        /// </summary>
        [DomName("changedTouches")]
        public ITouchList ChangedTouches
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

        #endregion
    }
}
