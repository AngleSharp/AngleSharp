namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the event args for any UI event.
    /// </summary>
    [DomName("UIEvent")]
    public class UiEvent : Event
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public UiEvent()
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
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public UiEvent(String type, Boolean bubbles = false, Boolean cancelable = false, IWindow view = null, Int32 detail = 0)
        {
            Init(type, bubbles, cancelable, view, detail);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated view.
        /// </summary>
        [DomName("view")]
        public IWindow View
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the event details.
        /// </summary>
        [DomName("detail")]
        public Int32 Detail
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the UI event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="detail">Sets the detail id for the UIevent.</param>
        [DomName("initUIEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail)
        {
            Init(type, bubbles, cancelable);
            View = view;
            Detail = detail;
        }

        #endregion
    }
}
