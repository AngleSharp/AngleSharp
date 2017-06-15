namespace AngleSharp.Io
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a useful abstraction for requesters.
    /// </summary>
    public abstract class BaseRequester : EventTarget, IRequester
    {
        /// <summary>
        /// Fired when a request is starting.
        /// </summary>
        public event DomEventHandler Requesting
        {
            add { AddEventListener(EventNames.Requesting, value); }
            remove { RemoveEventListener(EventNames.Requesting, value); }
        }

        /// <summary>
        /// Fired when a request is finished.
        /// </summary>
        public event DomEventHandler Requested
        {
            add { AddEventListener(EventNames.Requested, value); }
            remove { RemoveEventListener( EventNames.Requested, value); }
        }

        /// <summary>
        /// Performs an asynchronous request that can be cancelled.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <param name="cancel">The token for cancelling the task.</param>
        /// <returns>
        /// The task that will eventually give the response data.
        /// </returns>
        public async Task<IResponse> RequestAsync(Request request, CancellationToken cancel)
        {
            var ev = new RequestEvent(request, null);
            InvokeEventListener(ev);
            var response = await PerformRequestAsync(request, cancel);
            ev = new RequestEvent(request, response);
            InvokeEventListener(ev);
            return response;
        }

        /// <summary>
        /// Checks if the given protocol is supported.
        /// </summary>
        /// <param name="protocol">
        /// The protocol to check for, e.g. http.
        /// </param>
        /// <returns>
        /// True if the protocol is supported, otherwise false.
        /// </returns>
        public abstract Boolean SupportsProtocol(String protocol);

        /// <summary>
        /// Performs the actual request asynchronously.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <param name="cancel">The token for cancelling the task.</param>
        /// <returns>The task resulting in the response.</returns>
        protected abstract Task<IResponse> PerformRequestAsync(Request request, CancellationToken cancel);
    }
}
