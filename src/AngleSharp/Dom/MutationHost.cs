namespace AngleSharp.Dom
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Couples the mutation events to mutation observers and the event loop.
    /// </summary>
    sealed class MutationHost
    {
        #region Fields

        readonly List<MutationObserver> _observers;
        readonly IEventLoop _loop;
        Boolean _queued;

        #endregion

        #region ctor

        public MutationHost(IEventLoop loop)
        {
            _observers = new List<MutationObserver>();
            _queued = false;
            _loop = loop;
        }

        #endregion

        #region Properties

        public IEnumerable<MutationObserver> Observers
        {
            get { return _observers; }
        }

        #endregion

        #region Methods

        public void Register(MutationObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Unregister(MutationObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        public void ScheduleCallback()
        {
            if (!_queued)
            {
                _queued = true;
                _loop.Enqueue(DispatchCallback);
            }
        }

        void DispatchCallback()
        {
            var observers = _observers.ToArray();
            _queued = false;

            foreach (var observer in observers)
            {
                _loop.Enqueue(observer.Trigger, TaskPriority.Microtask);
            }
        }

        #endregion
    }
}
