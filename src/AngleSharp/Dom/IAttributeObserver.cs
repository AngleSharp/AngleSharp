namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Represents the interface for observing attribute changes.
    /// </summary>
    public interface IAttributeObserver
    {
        /// <summary>
        /// Defines the callback signature to react once an attribute changes.
        /// </summary>
        /// <param name="host">The element hosting the attribute.</param>
        /// <param name="name">The name of the changed attribute.</param>
        /// <param name="value">The new value of the attribute.</param>
        void NotifyChange(IElement host, String name, String value);
    }
}
