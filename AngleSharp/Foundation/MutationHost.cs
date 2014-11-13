namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.Extensions;
    using AngleSharp.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Couples the mutation events to mutation observers and the event loop.
    /// </summary>
    sealed class MutationHost
    {
        #region Fields

        readonly List<MutationObserver> _observers;
        readonly IEventService _eventLoop;
        readonly IBrowsingContext _context;
        Boolean _queued;

        #endregion

        #region ctor

        public MutationHost(IBrowsingContext context)
        {
            _context = context;
            _eventLoop = context.Configuration.GetService<IEventService>();
            _observers = new List<MutationObserver>();
            _queued = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Enqueues the flushing of the mutation observers in the event loop.
        /// </summary>
        public void Enqueue()
        {
            if (_queued)
                return;

            _queued = true;
            _eventLoop.Enqueue(new MicroDomTask(_context.Current.Document, (Func<Task>)Notify));
        }

        /// <summary>
        /// Notifies the registered observers with all registered changes.
        /// </summary>
        /// <returns>The awaitable task.</returns>
        public async Task Notify()
        {
            _queued = false;
            var notifyList = _observers.ToArray();

            foreach (var mo in notifyList)
            {
                await _eventLoop.Execute(() =>
                {
                    var queue = mo.Flush().ToArray();

                    //TODO Mutation
                    //Remove all transient registered observers whose observer is mo.

                    if (queue.Length != 0)
                        mo.TriggerWith(queue);
                });
            }
        }

        #endregion
    }
}
