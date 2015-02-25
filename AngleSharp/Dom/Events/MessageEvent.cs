namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the event arguments when receiving a message.
    /// </summary>
    [DomName("MessageEvent")]
    public class MessageEvent : Event
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public MessageEvent()
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="data">Sets the data for the message event.</param>
        /// <param name="origin">Sets the origin who send the message.</param>
        /// <param name="lastEventId">Sets the id of the last event.</param>
        /// <param name="source">Sets the source window of the message.</param>
        /// <param name="ports">The message ports to include.</param>
        public MessageEvent(String type, Boolean bubbles, Boolean cancelable, Object data, String origin, String lastEventId, IWindow source, params IMessagePort[] ports)
        {
            Init(type, bubbles, cancelable, data, origin, lastEventId, source, ports);
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="eventInitDict">
        /// An optional dictionary with optional keys such as
        /// bubbles (boolean) and cancelable (boolean).
        /// </param>
        [DomConstructor]
        public MessageEvent(String type, IDictionary<String, Object> eventInitDict = null)
            : base(type, eventInitDict)
        {
            Data = eventInitDict.TryGet("data");
            Origin = (eventInitDict.TryGet("origin") ?? String.Empty).ToString();
            LastEventId = (eventInitDict.TryGet("lastEventId") ?? String.Empty).ToString();
            Source = eventInitDict.TryGet("source") as IWindow;
            Ports = eventInitDict.TryGet("ports") as IMessagePort[] ?? new IMessagePort[0];
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data that is carried by the message.
        /// </summary>
        [DomName("data")]
        public Object Data
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the origin of the message.
        /// </summary>
        [DomName("origin")]
        public String Origin
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the id of the last event.
        /// </summary>
        [DomName("lastEventId")]
        public String LastEventId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the source of the message.
        /// </summary>
        [DomName("source")]
        public IWindow Source
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the used message ports.
        /// </summary>
        [DomName("ports")]
        public IMessagePort[] Ports
        {
            get;
            private set;
        }

        #endregion

        #region Methods

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
        /// <param name="ports">The message ports to include.</param>
        [DomName("initMessageEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable, Object data, String origin, String lastEventId, IWindow source, params IMessagePort[] ports)
        {
            Init(type, bubbles, cancelable);
            Data = data;
            Origin = origin;
            LastEventId = lastEventId;
            Source = source;
            Ports = ports;
        }

        #endregion
    }
}
