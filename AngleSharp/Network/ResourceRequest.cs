namespace AngleSharp.Network
{
    using System;

    /// <summary>
    /// Represents the arguments to load a resource.
    /// </summary>
    public class ResourceRequest
    {
        /// <summary>
        /// Creates a new resource request for the given url.
        /// </summary>
        /// <param name="target">The resource's url.</param>
        public ResourceRequest(Url target)
        {
            Target = target;
            IsManualRedirectDesired = false;
            IsSameOriginForced = false;
            IsCookieBlocked = false;
        }

        /// <summary>
        /// Gets the target of the request.
        /// </summary>
        public Url Target
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets if the manual redirect flag is active.
        /// </summary>
        public Boolean IsManualRedirectDesired
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the force same origin flag is active.
        /// </summary>
        public Boolean IsSameOriginForced
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the block cookies flag is active.
        /// </summary>
        public Boolean IsCookieBlocked
        {
            get;
            set;
        }
    }
}
