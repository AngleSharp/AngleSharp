namespace AngleSharp.Html.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Events;
    using System;

    /// <summary>
    /// Represents a popover or details state change event.
    /// </summary>
    [DomName("ToggleEvent")]
    [DomExposed("Window")]
    public class ToggleEvent : Event
    {
        /// <summary>
        /// Creates a new event and initializes it with the members of a ToggleEventInit.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event bubbles.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="oldState">The previous state.</param>
        /// <param name="newState">The new state.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public ToggleEvent(String type, Boolean bubbles = false, Boolean cancelable = false, String oldState = "closed", String newState = "closed")
            : base(type, bubbles, cancelable)
        {
            OldState = oldState;
            NewState = newState;
        }

        /// <summary>
        /// Gets the state before the change.
        /// </summary>
        [DomName("oldState")]
        public String OldState
        {
            get;
        }

        /// <summary>
        /// Gets the state after the change.
        /// </summary>
        [DomName("newState")]
        public String NewState
        {
            get;
        }
    }
}
