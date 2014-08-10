namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents a custom event that provides an additional details property.
    /// </summary>
    /// <typeparam name="T">The type of the details.</typeparam>
    sealed class CustomEvent : Event, ICustomEvent
    {
        #region Fields

        Object _details;

        #endregion

        #region Properties

        public Object Details
        {
            get { return _details; }
        }

        #endregion

        #region Methods

        public void Init(String type, Boolean bubbles, Boolean cancelable, Object details)
        {
            _details = details;
            Init(type, bubbles, cancelable);
        }

        #endregion
    }
}
