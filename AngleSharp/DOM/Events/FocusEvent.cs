namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents the arguments for a focus event.
    /// </summary>
    sealed class FocusEvent : UiEvent, IFocusEvent
    {
        #region Properties

        public IEventTarget Target
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail, IEventTarget target)
        {
            Init(type, bubbles, cancelable, view, detail);
            Target = target;
        }

        #endregion
    }
}
