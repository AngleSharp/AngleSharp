namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the arguments for a focus event.
    /// </summary>
    [DomName("FocusEvent")]
    public class FocusEvent : UiEvent
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public FocusEvent()
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
        /// <param name="target">The target that is being focused.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public FocusEvent(String type, Boolean bubbles = false, Boolean cancelable = false, IWindow view = null, Int32 detail = 0, IEventTarget target = null)
        {
            Init(type, bubbles, cancelable, view, detail, target);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the target of the event.
        /// </summary>
        [DomName("relatedTarget")]
        public IEventTarget Target
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
        /// <param name="target">The target that is being focused.</param>
        [DomName("initFocusEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail, IEventTarget target)
        {
            Init(type, bubbles, cancelable, view, detail);
            Target = target;
        }

        #endregion
    }
}
