namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents the event arguments for a hash changed event.
    /// </summary>
    [DomName("HashChangeEvent")]
    public interface IHashChangeEvent : IEvent
    {
        /// <summary>
        /// Gets the URL before the hash changed.
        /// </summary>
        [DomName("oldURL")]
        String PreviousUrl { get; }

        /// <summary>
        /// Gets the URL after the hash changed.
        /// </summary>
        [DomName("newURL")]
        String CurrentUrl { get; }
    }
}
