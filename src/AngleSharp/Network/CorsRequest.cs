namespace AngleSharp.Network
{
    using AngleSharp.Services;

    /// <summary>
    /// Represents the arguments to perform a fetch with CORS.
    /// </summary>
    public class CorsRequest
    {
        /// <summary>
        /// Creates a new CORS enabled request.
        /// </summary>
        /// <param name="request">The original request.</param>
        public CorsRequest(ResourceRequest request)
        {
            Request = request;
        }

        /// <summary>
        /// Gets the original request to perform.
        /// </summary>
        public ResourceRequest Request
        {
            get;
        }

        /// <summary>
        /// Gets or sets the CORS settings to use.
        /// </summary>
        public CorsSetting Setting
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the behavior in case of no CORS.
        /// </summary>
        public OriginBehavior Behavior
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the integrity provider, if any.
        /// </summary>
        public IIntegrityProvider Integrity
        {
            get;
            set;
        }
    }
}
