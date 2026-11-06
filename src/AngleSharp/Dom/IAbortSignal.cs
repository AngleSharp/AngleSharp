namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// A signal that can stop what was given it, implemented by whoever provides one, e.g.,
    /// AngleSharp.Io.
    /// </summary>
    [DomNoInterfaceObject]
    public interface IAbortSignal : IEventTarget
    {
        /// <summary>
        /// Gets if the signal is aborted. It tells so by firing an abort event.
        /// </summary>
        Boolean IsAborted { get; }
    }
}
