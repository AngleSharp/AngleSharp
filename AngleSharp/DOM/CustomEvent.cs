namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents a custom event that provides an additional details property.
    /// </summary>
    /// <typeparam name="T">The type of the details.</typeparam>
    sealed class CustomEvent<T> : Event, ICustomEvent<T>
    {
        #region Fields

        T _details;

        #endregion

        #region Properties

        public T Details
        {
            get { return _details; }
        }

        #endregion

        #region Methods

        public void Init(String type, Boolean bubbles, Boolean cancelable, T details)
        {
            _details = details;
            Init(type, bubbles, cancelable);
        }

        #endregion
    }
}
