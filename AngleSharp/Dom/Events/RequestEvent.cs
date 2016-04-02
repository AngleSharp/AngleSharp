namespace AngleSharp.Dom.Events
{
    using AngleSharp.Html;
    using AngleSharp.Network;

    /// <summary>
    /// The event that is published in case of new request.
    /// </summary>
    public class RequestEvent : Event
    {
        /// <summary>
        /// Creates a new event for a request.
        /// </summary>
        /// <param name="request">The data of the request.</param>
        /// <param name="response">The received response.</param>
        public RequestEvent(IRequest request, IResponse response)
            : base(response != null ? EventNames.RequestEnd : EventNames.RequestStart)
        {
            Response = response;
            Request = request;
        }

        /// <summary>
        /// Gets the transmitted request.
        /// </summary>
        public IRequest Request
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the received response.
        /// </summary>
        public IResponse Response
        {
            get;
            private set;
        }
    }
}
