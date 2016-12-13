namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a message port as defined here:
    /// http://www.whatwg.org/specs/web-apps/current-work/multipage/comms.html#messageport
    /// </summary>
    [DomName("MessagePort")]
    public interface IMessagePort : IEventTarget
    {
        /// <summary>
        /// Sends a message over the current message port.
        /// </summary>
        /// <param name="message">The message to send. Will be serialized to a string.</param>
        [DomName("postMessage")]
        void Send(Object message);
        
        /// <summary>
        /// Opens the message port.
        /// </summary>
        [DomName("start")]
        void Open();
        
        /// <summary>
        /// Closes the message port.
        /// </summary>
        [DomName("close")]
        void Close();
        
        /// <summary>
        /// Fired when a message has been received.
        /// </summary>
        [DomName("onmessage")]
        event DomEventHandler MessageReceived;
    }
}
