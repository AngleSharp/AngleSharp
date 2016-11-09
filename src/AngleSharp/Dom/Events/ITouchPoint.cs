namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the interface for the data of a single touch point.
    /// </summary>
    [DomName("Touch")]
    public interface ITouchPoint
    {
        /// <summary>
        /// Gets the id of the touch point.
        /// </summary>
        [DomName("identifier")]
        Int32 Id { get; }

        /// <summary>
        /// Gets the target of the touch point.
        /// </summary>
        [DomName("target")]
        IEventTarget Target { get; }

        /// <summary>
        /// Gets the x-coordinate relative to the screen.
        /// </summary>
        [DomName("screenX")]
        Int32 ScreenX { get; }

        /// <summary>
        /// Gets the y-coordinate relative to the screen.
        /// </summary>
        [DomName("screenY")]
        Int32 ScreenY { get; }

        /// <summary>
        /// Gets the x-coordinate relative to the client.
        /// </summary>
        [DomName("clientX")]
        Int32 ClientX { get; }

        /// <summary>
        /// Gets the y-coordinate relative to the client.
        /// </summary>
        [DomName("clientY")]
        Int32 ClientY { get; }

        /// <summary>
        /// Gets the x-coordinate relative to the page.
        /// </summary>
        [DomName("pageX")]
        Int32 PageX { get; }

        /// <summary>
        /// Gets the y-coordinate relative to the page.
        /// </summary>
        [DomName("pageY")]
        Int32 PageY { get; }
    }
}
