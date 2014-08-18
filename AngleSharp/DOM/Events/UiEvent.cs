namespace AngleSharp.DOM.Events
{
    using System;

    class UiEvent : Event, IUiEvent
    {
        #region Properties

        public IWindow View
        {
            get;
            private set;
        }

        public Int32 Detail
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, Int32 detail)
        {
            Init(type, bubbles, cancelable);
            View = view;
            Detail = detail;
        }

        #endregion
    }
}
