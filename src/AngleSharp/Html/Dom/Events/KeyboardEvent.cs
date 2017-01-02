﻿namespace AngleSharp.Html.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;

    /// <summary>
    /// Represents the event arguments for a keyboard event.
    /// </summary>
    [DomName("KeyboardEvent")]
    public class KeyboardEvent : UiEvent
    {
        #region Fields

        private String _modifiers;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public KeyboardEvent()
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="detail">Sets the detail id for the UI event.</param>
        /// <param name="key">Sets the key that is currently pressed.</param>
        /// <param name="location">Sets the position of the originating keyboard.</param>
        /// <param name="modifiersList">A list with keyboard modifiers that have been pressed.</param>
        /// <param name="repeat">Sets if the key has been pressed again.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public KeyboardEvent(String type, Boolean bubbles = false, Boolean cancelable = false, IWindow view = null, Int32 detail = 0, String key = null, KeyboardLocation location = KeyboardLocation.Standard, String modifiersList = null, Boolean repeat = false)
        {
            Init(type, bubbles, cancelable, view, detail, key ?? String.Empty, location, modifiersList ?? String.Empty, repeat);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets string representation of the pressed key.
        /// </summary>
        [DomName("key")]
        public String Key
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the location of the keyboard that initiated the event.
        /// </summary>
        [DomName("location")]
        public KeyboardLocation Location
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets if the control key is pressed.
        /// </summary>
        [DomName("ctrlKey")]
        public Boolean IsCtrlPressed
        {
            get { return _modifiers.IsCtrlPressed(); }
        }

        /// <summary>
        /// Gets if the shift key is pressed.
        /// </summary>
        [DomName("shiftKey")]
        public Boolean IsShiftPressed
        {
            get { return _modifiers.IsShiftPressed(); }
        }

        /// <summary>
        /// Gets if the alt key is pressed.
        /// </summary>
        [DomName("altKey")]
        public Boolean IsAltPressed
        {
            get { return _modifiers.IsAltPressed(); }
        }

        /// <summary>
        /// Gets if the meta key is pressed.
        /// </summary>
        [DomName("metaKey")]
        public Boolean IsMetaPressed
        {
            get { return _modifiers.IsMetaPressed(); }
        }

        /// <summary>
        /// Gets if the key press was repeated.
        /// </summary>
        [DomName("repeat")]
        public Boolean IsRepeated
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the current state of the specified modifier key.
        /// </summary>
        /// <param name="key">The modifier key to lookup.</param>
        /// <returns>True if the key is currently pressed, otherwise false.</returns>
        [DomName("getModifierState")]
        public Boolean GetModifierState(String key)
        {
            return _modifiers.ContainsKey(key);
        }

        /// <summary>
        /// Gets the locale of the keyboard.
        /// </summary>
        [DomName("locale")]
        public String Locale
        {
            get { return IsTrusted ? String.Empty : null; }
        }

        #endregion

        #region Methods

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
        public void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail, String key, KeyboardLocation location, String modifiersList, Boolean repeat)
        {
            Init(type, bubbles, cancelable, view, detail);
            Key = key;
            Location = location;
            IsRepeated = repeat;
            _modifiers = modifiersList;
        }

        #endregion
    }
}
