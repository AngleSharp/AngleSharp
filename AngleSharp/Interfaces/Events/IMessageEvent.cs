namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents the event arguments when receiving a message.
    /// </summary>
    [DomName("MessageEvent")]
    public interface IMessageEvent : IEvent
    {
        /// <summary>
        /// Gets the data that is carried by the message.
        /// </summary>
        [DomName("data")]
        Object Data { get; }

        /// <summary>
        /// Gets the origin of the message.
        /// </summary>
        [DomName("origin")]
        String Origin { get; }

        /// <summary>
        /// Gets the id of the last event.
        /// </summary>
        [DomName("lastEventId")]
        String LastEventId { get; }

        /// <summary>
        /// Gets the source of the message.
        /// </summary>
        [DomName("source")]
        IWindowProxy Source { get; }

        //TODO http://www.whatwg.org/specs/web-apps/current-work/multipage/comms.html#messageport
        //[DomName("ports")]
        //MessagePort[]? Ports {get;}

        /// <summary>
        /// Initializes the message event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="data">Sets the data for the message event.</param>
        /// <param name="origin">Sets the origin who send the message.</param>
        /// <param name="lastEventId">Sets the id of the last event.</param>
        /// <param name="source">Sets the source window of the message.</param>
        [DomName("initMessageEvent")]
        void Init(String type, Boolean bubbles, Boolean cancelable, Object data, String origin, String lastEventId, IWindowProxy source/*, params MessagePort[] portsArg*/);
    }
}
