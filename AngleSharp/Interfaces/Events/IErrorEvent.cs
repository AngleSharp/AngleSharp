namespace AngleSharp.DOM.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the event arguments for an error event.
    /// </summary>
    [DomName("ErrorEvent")]
    public interface IErrorEvent : IEvent
    {
        /// <summary>
        /// Gets the message describing the error.
        /// </summary>
        [DomName("message")]
        String Message { get; }

        /// <summary>
        /// Gets the filename where the error occurred.
        /// </summary>
        [DomName("filename")]
        String FileName { get; }

        /// <summary>
        /// Gets the line number of the error.
        /// </summary>
        [DomName("lineno")]
        Int32 Line { get; }

        /// <summary>
        /// Gets the column number of the error.
        /// </summary>
        [DomName("colno")]
        Int32 Column { get; }

        /// <summary>
        /// Gets a custom error information object.
        /// </summary>
        [DomName("error")]
        Object ErrorInformation { get; }
    }
}
