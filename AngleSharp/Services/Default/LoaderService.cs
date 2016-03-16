namespace AngleSharp.Services.Default
{
    using AngleSharp.Network;
    using AngleSharp.Network.Default;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the default loader service. This class can be inherited.
    /// </summary>
    public class LoaderService : ILoaderService
    {
        #region Fields

        readonly IEnumerable<IRequester> _requesters;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new loader service with the provided requesters.
        /// </summary>
        /// <param name="requesters">The requesters to use.</param>
        public LoaderService(IEnumerable<IRequester> requesters)
        {
            _requesters = requesters;
            IsNavigationEnabled = true;
            IsResourceLoadingEnabled = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the available requesters.
        /// </summary>
        public IEnumerable<IRequester> Requesters
        {
            get { return _requesters; }
        }

        /// <summary>
        /// Gets or sets of navigation is enabled.
        /// </summary>
        public Boolean IsNavigationEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if resource loading is enabled.
        /// </summary>
        public Boolean IsResourceLoadingEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the filter to use for outgoing requests.
        /// </summary>
        public Predicate<IRequest> Filter
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the appropriate requester for the provided address.
        /// </summary>
        /// <param name="address">
        /// The address the requester needs to be able to handle.
        /// </param>
        /// <returns>The requester or null.</returns>
        public IRequester GetRequester(Url address)
        {
            foreach (var requester in _requesters)
            {
                if (requester.SupportsProtocol(address.Scheme))
                {
                    return requester;
                }
            }

            return default(IRequester);
        }

        /// <summary>
        /// Creates the default document loader with the stored requesters.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <returns>The instantiated default document loader.</returns>
        public virtual IDocumentLoader CreateDocumentLoader(IBrowsingContext context)
        {
            if (!IsNavigationEnabled)
            {
                return null;
            }

            return new DocumentLoader(_requesters, context.Configuration, Filter);
        }

        /// <summary>
        /// Creates the default resource loader with the stored requesters.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <returns>The instantiated default resource loader.</returns>
        public virtual IResourceLoader CreateResourceLoader(IBrowsingContext context)
        {
            if (!IsResourceLoadingEnabled)
            {
                return null;
            }

            return new ResourceLoader(_requesters, context.Configuration, Filter);
        }

        #endregion
    }
}
