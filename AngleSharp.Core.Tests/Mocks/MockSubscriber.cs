namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Events;
    using System;

    class MockSubscriber<T> : ISubscriber<T>
    {
        readonly Action<T> _listener;

        public MockSubscriber(Action<T> listener)
        {
            _listener = listener;
        }

        public void OnEventData(T data)
        {
            _listener(data);
        }
    }
}
