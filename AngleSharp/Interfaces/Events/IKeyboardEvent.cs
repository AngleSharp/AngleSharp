namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents the event arguments for a keyboard event.
    /// </summary>
    [DomName("KeyboardEvent")]
    public interface IKeyboardEvent : IUiEvent
    {
        /// <summary>
        /// Gets string representation of the pressed key.
        /// </summary>
        [DomName("key")]
        String Key { get; }

        /// <summary>
        /// Gets the location of the keyboard that initiated the event.
        /// </summary>
        [DomName("location")]
        KeyboardLocation Location { get; }

        /// <summary>
        /// Gets if the control key is pressed.
        /// </summary>
        [DomName("ctrlKey")]
        Boolean IsCtrlPressed { get; }

        /// <summary>
        /// Gets if the shift key is pressed.
        /// </summary>
        [DomName("shiftKey")]
        Boolean IsShiftPressed { get; }

        /// <summary>
        /// Gets if the alt key is pressed.
        /// </summary>
        [DomName("altKey")]
        Boolean IsAltPressed { get; }

        /// <summary>
        /// Gets if the meta key is pressed.
        /// </summary>
        [DomName("metaKey")]
        Boolean IsMetaPressed { get; }

        /// <summary>
        /// Gets if the key press was repeated.
        /// </summary>
        [DomName("repeat")]
        Boolean IsRepeated { get; }

        /// <summary>
        /// Returns the current state of the specified modifier key.
        /// </summary>
        /// <param name="key">The modifier key to lookup.</param>
        /// <returns>True if the key is currently pressed, otherwise false.</returns>
        [DomName("getModifierState")]
        Boolean GetModifierState(String key);

        /// <summary>
        /// Gets the locale of the keyboard.
        /// </summary>
        [DomName("locale")]
        String Locale { get; }

        /// <summary>
        /// Initializes the keyboard event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="detail">Sets the detail id for the UI event.</param>
        /// <param name="key">Sets the key that is currently pressed.</param>
        /// <param name="location">Sets the position of the originating keyboard.</param>
        /// <param name="modifiersList">A list with keyboard modifiers that have been pressed.</param>
        /// <param name="repeat">Sets if the key has been pressed again.</param>
        [DomName("initKeyboardEvent")]
        void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail, String key, KeyboardLocation location, String modifiersList, Boolean repeat);
    }
}
