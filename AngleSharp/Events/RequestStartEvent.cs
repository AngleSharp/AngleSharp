namespace AngleSharp.Events
{
    using System;
    using AngleSharp.Network;

    /// <summary>
    /// The event that is published in case of new request.
    /// </summary>
    public class RequestStartEvent
    {
        /// <summary>
        /// Action called once the request ended.
        /// </summary>
        public event Action<IResponse> Ended;

        /// <summary>
        /// Creates a new event for starting a request.
        /// </summary>
        /// <param name="requester">The associated requester.</param>
        /// <param name="request">The data of the request.</param>
        public RequestStartEvent(IRequester requester, IRequest request)
        {
            Requester = requester;
            Request = request;
        }

        /// <summary>
        /// Gets the associated requester.
        /// </summary>
        public IRequester Requester
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the request data to transmit.
        /// </summary>
        public IRequest Request
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the response by invoking the ended event.
        /// </summary>
        /// <param name="response">The response to propagate.</param>
        public void SetResult(IResponse response)
        {
            if (Ended != null)
                Ended(response);
        }
    }
}
