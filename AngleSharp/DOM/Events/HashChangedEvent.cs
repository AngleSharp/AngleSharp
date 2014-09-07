namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents a custom event that provides an additional details property.
    /// </summary>
    class HashChangedEvent : Event, IHashChangeEvent
    {
        #region Properties

        public String PreviousUrl
        {
            get;
            private set;
        }

        public String CurrentUrl
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Init(String type, Boolean bubbles, Boolean cancelable, String previousUrl, String currentUrl)
        {
            Init(type, bubbles, cancelable);
            PreviousUrl = previousUrl;
            CurrentUrl = currentUrl;
        }

        #endregion
    }
}
