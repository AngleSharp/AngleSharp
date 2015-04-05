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
        /// <param name="request">The data of the request.</param>
        /// <param name="response">The received response.</param>
        public RequestEndEvent(IRequest request, IResponse response)
        {
            Request = request;
            Response = response;
        }

        /// <summary>
        /// Gets the request data that has been transmitted.
        /// </summary>
        public IRequest Request
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the response to the transmitted request.
        /// </summary>
        public IResponse Response
        {
            get;
            private set;
        }
    }
}
