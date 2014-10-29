namespace AngleSharp.DOM.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the event arguments for an UI event.
    /// </summary>
    [DomName("UIEvent")]
    public interface IUiEvent : IEvent
    {
        /// <summary>
        /// Gets the associated view.
        /// </summary>
        [DomName("view")]
        IWindow View { get; }

        /// <summary>
        /// Gets the event details.
        /// </summary>
        [DomName("detail")]
        Int32 Detail { get; }

        /// <summary>
        /// Initializes the UI event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="detail">Sets the detail id for the UIevent.</param>
        [DomName("initUIEvent")]
        void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail);
    }
}
