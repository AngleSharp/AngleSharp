namespace AngleSharp.Core.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Events;

    class EventReceiver<TReceivingEvent> : IEventAggregator
    {
        readonly List<TReceivingEvent> _received = new List<TReceivingEvent>();

        public List<TReceivingEvent> Received
        {
            get { return _received; }
        }

        public void Publish<TEvent>(TEvent data)
        {
            if (typeof(TEvent) == typeof(TReceivingEvent))
                _received.Add((TReceivingEvent)(data as Object));
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
