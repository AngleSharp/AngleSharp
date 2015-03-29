namespace AngleSharp.Events
{
    using AngleSharp.Network;

    /// <summary>
    /// The event that is published in case of a finished request.
    /// </summary>
    public class RequestEndEvent
    {
        /// <summary>
        /// Creates a new event for finishing a request.
        /// </summary>
        /// <param name="requester">The associated requester.</param>
        /// <param name="request">The data of the request.</param>
        public RequestEndEvent(IRequester requester, IRequest request)
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
        /// Gets the request data that has been transmitted.
        /// </summary>
        public IRequest Request
        {
            get;
            private set;
        }
    }
}
