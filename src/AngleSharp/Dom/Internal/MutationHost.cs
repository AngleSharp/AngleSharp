namespace AngleSharp.Dom
{
    using AngleSharp.Browser;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Couples the mutation events to mutation observers and the event loop.
    /// </summary>
    sealed class MutationHost
    {
        #region Fields

        private readonly List<MutationObserver> _observers;
        private readonly IEventLoop _loop;
        private Boolean _queued;

        #endregion

        #region ctor

        public MutationHost(IEventLoop loop)
        {
            _observers = [];
            _queued = false;
            _loop = loop;
        }

        #endregion

        #region Properties

        public IEnumerable<MutationObserver> Observers => _observers;

        /// <summary>
        /// Gets whether any observer is registered. Callers use this to skip building a record that
        /// nothing would consume - a mutation record costs an allocation on every single mutation.
        /// </summary>
        public Boolean HasObservers => _observers.Count > 0;

        #endregion

        #region Methods

        /// <summary>
        /// Takes the snapshot the dispatch iterates. An observer callback is free to connect or
        /// disconnect while records are being delivered, so the list itself must not be walked.
        /// </summary>
        public MutationObserver[] Snapshot() => _observers.ToArray();

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

        private void DispatchCallback()
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
