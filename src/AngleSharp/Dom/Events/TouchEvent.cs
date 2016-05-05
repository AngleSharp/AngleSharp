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
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public TouchEvent()
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="detail">Sets the detail id for the UIevent.</param>
        /// <param name="touches">The list of active touches.</param>
        /// <param name="targetTouches">The list of target-active toches.</param>
        /// <param name="changedTouches">The list of changed touches.</param>
        /// <param name="ctrlKey">Sets if the control key was pressed.</param>
        /// <param name="altKey">Sets if the alt key was pressed.</param>
        /// <param name="shiftKey">Sets if the shift key was pressed.</param>
        /// <param name="metaKey">Sets if the meta key was pressed.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public TouchEvent(String type, Boolean bubbles = false, Boolean cancelable = false, IWindow view = null, Int32 detail = 0, ITouchList touches = null, ITouchList targetTouches = null, ITouchList changedTouches = null, Boolean ctrlKey = false, Boolean altKey = false, Boolean shiftKey = false, Boolean metaKey = false)
        {
            Init(type, bubbles, cancelable, view, detail);
        }

        #endregion

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

        #region Methods

        /// <summary>
        /// Initializes the focus event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="detail">Sets the detail id for the UIevent.</param>
        /// <param name="touches">The list of active touches.</param>
        /// <param name="targetTouches">The list of target-active toches.</param>
        /// <param name="changedTouches">The list of changed touches.</param>
        /// <param name="ctrlKey">Sets if the control key was pressed.</param>
        /// <param name="altKey">Sets if the alt key was pressed.</param>
        /// <param name="shiftKey">Sets if the shift key was pressed.</param>
        /// <param name="metaKey">Sets if the meta key was pressed.</param>
        [DomName("initTouchEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail, ITouchList touches, ITouchList targetTouches, ITouchList changedTouches, Boolean ctrlKey, Boolean altKey, Boolean shiftKey, Boolean metaKey)
        {
            Init(type, bubbles, cancelable, view, detail);
            Touches = touches;
            TargetTouches = targetTouches;
            ChangedTouches = changedTouches;
            IsCtrlPressed = ctrlKey;
            IsShiftPressed = shiftKey;
            IsMetaPressed = metaKey;
            IsAltPressed = altKey;
        }

        #endregion
    }
}
