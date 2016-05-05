namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;
    using System.Collections.Generic;

    class EventReceiver<TReceivingEvent>
        where TReceivingEvent : Event
    {
        readonly List<TReceivingEvent> _received = new List<TReceivingEvent>();

        public EventReceiver(Action<DomEventHandler> addHandler)
        {
            addHandler((s, ev) =>
            {
                var data = ev as TReceivingEvent;

                if (data != null)
                {
                    Receive(data);
                }
            });
        }

        public List<TReceivingEvent> Received
        {
            get { return _received; }
        }

        public Action<TReceivingEvent> OnReceived
        {
            get;
            set;
        }

        void Receive(TReceivingEvent data)
        {
            if (OnReceived != null)
            {
                OnReceived(data);
            }

            _received.Add(data);
        }
    }
}
