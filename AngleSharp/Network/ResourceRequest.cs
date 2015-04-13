namespace AngleSharp.Network
{
    using System;
    using AngleSharp.Dom;

    /// <summary>
    /// Represents the arguments to load a resource.
    /// </summary>
    public class ResourceRequest
    {
        /// <summary>
        /// Creates a new resource request for the given url.
        /// </summary>
        /// <param name="source">The request's source.</param>
        /// <param name="target">The resource's url.</param>
        public ResourceRequest(IElement source, Url target)
        {
            Source = source;
            Target = target;
            Origin = source.Owner.Origin;
            IsManualRedirectDesired = false;
            IsSameOriginForced = false;
            IsCookieBlocked = false;
            IsCredentialOmitted = false;
        }

        /// <summary>
        /// Gets the source of the request.
        /// </summary>
        public IElement Source
        {
            get;
            private set;
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
        /// Gets or sets the origin of the request, if any.
        /// </summary>
        public String Origin
        {
            get;
            set;
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
        /// Gets or sets if the omit credentials flag is active.
        /// </summary>
        public Boolean IsCredentialOmitted
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
