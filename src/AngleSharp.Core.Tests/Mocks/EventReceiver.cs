namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;
    using System.Collections.Generic;

    class EventReceiver<TReceivingEvent>
        where TReceivingEvent : Event
    {
        private readonly List<TReceivingEvent> _received = new List<TReceivingEvent>();

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

        private void Receive(TReceivingEvent data)
        {
            OnReceived?.Invoke(data);

            _received.Add(data);
        }
    }
}
