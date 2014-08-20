namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents the event arguments for a keyboard event.
    /// </summary>
    class KeyboardEvent : UiEvent, IKeyboardEvent
    {
        #region Fields

        String _modifiers;

        #endregion

        #region Properties

        public String Key
        {
            get;
            private set;
        }

        public KeyboardLocation Location
        {
            get;
            private set;
        }

        public Boolean IsCtrlPressed
        {
            get { return _modifiers.IsCtrlPressed(); }
        }

        public Boolean IsShiftPressed
        {
            get { return _modifiers.IsShiftPressed(); }
        }

        public Boolean IsAltPressed
        {
            get { return _modifiers.IsAltPressed(); }
        }

        public Boolean IsMetaPressed
        {
            get { return _modifiers.IsMetaPressed(); }
        }

        public Boolean IsRepeated
        {
            get;
            private set;
        }

        public Boolean GetModifierState(String key)
        {
            return _modifiers.ContainsKey(key);
        }

        public String Locale
        {
            get { return IsTrusted ? String.Empty : null; }
        }

        #endregion

        #region Methods

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
