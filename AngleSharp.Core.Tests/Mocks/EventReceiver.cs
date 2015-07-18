namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Events;
    using System;
    using System.Collections.Generic;

    class EventReceiver<TReceivingEvent> : IEventAggregator
    {
        readonly List<TReceivingEvent> _received = new List<TReceivingEvent>();

        public List<TReceivingEvent> Received
        {
            get { return _received; }
        }

        public Action<TReceivingEvent> OnReceived
        {
            get;
            set;
        }

        public void Publish<TEvent>(TEvent data)
        {
            if (typeof(TEvent) == typeof(TReceivingEvent))
                Receive((TReceivingEvent)(data as Object));
        }

        void Receive(TReceivingEvent data)
        {
            if (OnReceived != null)
                OnReceived(data);

            _received.Add(data);
        }

        public void Subscribe<TEvent>(ISubscriber<TEvent> listener)
        {
            //Empty on purpose
        }

        public void Unsubscribe<TEvent>(ISubscriber<TEvent> listener)
        {
            //Empty on purpose
        }
    }
}
