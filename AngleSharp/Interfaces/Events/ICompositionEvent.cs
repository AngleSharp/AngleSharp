namespace AngleSharp.DOM.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the event arguments for a composed event.
    /// </summary>
    [DomName("CompositionEvent")]
    public interface ICompositionEvent : IUiEvent
    {
        /// <summary>
        /// Gets the associated data.
        /// </summary>
        [DomName("data")]
        String Data { get; }

        /// <summary>
        /// Initializes the composition event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="data">Sets the data to carry.</param>
        [DomName("initCompositionEvent")]
        void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, String data);
    }
}
