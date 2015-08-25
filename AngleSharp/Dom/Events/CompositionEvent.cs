namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the event arguments for a composed event.
    /// </summary>
    [DomName("CompositionEvent")]
    public class CompositionEvent : UiEvent
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public CompositionEvent()
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="data">Sets the data to carry.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public CompositionEvent(String type, Boolean bubbles = false, Boolean cancelable = false, IWindow view = null, String data = null)
        {
            Init(type, bubbles, cancelable, view, data ?? String.Empty);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated data.
        /// </summary>
        [DomName("data")]
        public String Data
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the composition event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="data">Sets the data to carry.</param>
        [DomName("initCompositionEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, String data)
        {
            Init(type, bubbles, cancelable, view, 0);
            Data = data;
        }

        #endregion
    }
}
