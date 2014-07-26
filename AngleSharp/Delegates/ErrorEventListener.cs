namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Defines the callback signature for an error event.
    /// </summary>
    /// <param name="ev">The event arguments.</param>
    /// <param name="source">The source file.</param>
    /// <param name="line">The line in the source.</param>
    /// <param name="column">The column in the source.</param>
    /// <param name="error">The specific error.</param>
    [DomName("OnErrorEventHandler")]
    public delegate void ErrorEventListener(IEvent ev, String source = null, Int32? line = null, Int32? column = null, DomException error = null);
}
