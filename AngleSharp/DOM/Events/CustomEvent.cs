namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents a custom event that provides an additional details property.
    /// </summary>
    class CustomEvent : Event, ICustomEvent
    {
        #region Properties

        public Object Details
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Init(String type, Boolean bubbles, Boolean cancelable, Object details)
        {
            Init(type, bubbles, cancelable);
            Details = details;
        }

        #endregion
    }
}
