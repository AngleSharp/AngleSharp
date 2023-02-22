namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;
    using System.Collections.Generic;

    class EventReceiver<TReceivingEvent>
        where TReceivingEvent : Event
    {
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

        public List<TReceivingEvent> Received { get; } = new List<TReceivingEvent>();

        public Action<TReceivingEvent> OnReceived
        {
            get;
            set;
        }

        private void Receive(TReceivingEvent data)
        {
            OnReceived?.Invoke(data);

            Received.Add(data);
        }
    }
}
