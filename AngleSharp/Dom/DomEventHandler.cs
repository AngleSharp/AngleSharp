namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Events;
    using System;

    /// <summary>
    /// Defines the callback signature for an event.
    /// </summary>
    /// <param name="sender">The callback this argument.</param>
    /// <param name="ev">The event arguments.</param>
    [DomName("EventHandler")]
    public delegate void DomEventHandler(Object sender, Event ev);
}
