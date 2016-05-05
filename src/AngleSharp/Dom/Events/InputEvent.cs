namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the event arguments for an input event.
    /// </summary>
    [DomName("InputEvent")]
    public class InputEvent : Event
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public InputEvent()
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="data">Sets the data for the input event.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public InputEvent(String type, Boolean bubbles = false, Boolean cancelable = false, String data = null)
        {
            Init(type, bubbles, cancelable, data ?? String.Empty);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data that has been entered.
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
        /// Initializes the input event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="data">Sets the data for the input event.</param>
        [DomName("initInputEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable, String data)
        {
            Init(type, bubbles, cancelable);
            Data = data;
        }

        #endregion
    }
}
