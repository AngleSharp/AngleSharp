namespace AngleSharp.DOM.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the event arguments for an input event.
    /// </summary>
    [DomName("InputEvent")]
    public interface IInputEvent : IEvent
    {
        /// <summary>
        /// Gets the data that has been entered.
        /// </summary>
        [DomName("data")]
        String Data { get; }

        /// <summary>
        /// Initializes the input event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="data">Sets the data for the input event.</param>
        [DomName("initInputEvent")]
        void Init(String type, Boolean bubbles, Boolean cancelable, String data);
    }
}
