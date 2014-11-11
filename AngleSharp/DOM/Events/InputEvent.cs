namespace AngleSharp.DOM.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the event arguments for an input event.
    /// </summary>
    [DomName("InputEvent")]
    public class InputEvent : Event
    {
        /// <summary>
        /// Gets the data that has been entered.
        /// </summary>
        [DomName("data")]
        public String Data
        {
            get;
            private set;
        }

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
    }
}
