namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.Extensions;
    using AngleSharp.Infrastructure;
    using AngleSharp.Services;
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
        readonly Document _document;
        Boolean _queued;

        #endregion

        #region ctor

        public MutationHost(Document document)
        {
            _observers = new List<MutationObserver>();
            _queued = false;
            _document = document;
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

            var context = _document.Context;

            if (context == null)
                return;

            var eventLoop = context.Configuration.GetService<IEventService>();

            if (eventLoop == null)
                return;

            _queued = true;
            Func<Task> task = Notify;
            eventLoop.Enqueue(new MicroDomTask(_document, task));
        }

        /// <summary>
        /// Notifies the registered observers with all registered changes.
        /// </summary>
        /// <returns>The awaitable task.</returns>
        public async Task Notify()
        {
            var notifyList = _observers.ToArray();
            var context = _document.Context;

            if (context == null)
                return;

            var eventLoop = context.Configuration.GetService<IEventService>();

            if (eventLoop == null)
                return;

            _queued = false;

            foreach (var mo in notifyList)
            {
                await eventLoop.Execute(() =>
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
