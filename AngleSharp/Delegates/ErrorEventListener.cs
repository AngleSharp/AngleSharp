namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Defines the callback signature for an error event.
    /// </summary>
    /// <param name="ev">The event arguments.</param>
    [DomName("OnErrorEventHandler")]
    public delegate void ErrorEventListener(IEvent ev, String source = null, Int32? line = null, Int32? column = null, DomException error = null);
}
