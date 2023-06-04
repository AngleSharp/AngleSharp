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
            addHandler((_, ev) =>
            {

                if (ev is TReceivingEvent data)
                {
                    Receive(data);
                }
            });
        }

        public List<TReceivingEvent> Received => _received;

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
