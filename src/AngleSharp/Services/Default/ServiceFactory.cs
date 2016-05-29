namespace AngleSharp.Services.Default
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// The default implementation of a service factory.
    /// </summary>
    public class ServiceFactory : IServiceFactory
    {
        /// <summary>
        /// Tries to create the given service.
        /// </summary>
        /// <typeparam name="TService">The type of service.</typeparam>
        /// <param name="context">The context to host the service.</param>
        /// <returns>The created service, if any.</returns>
        public TService Create<TService>(IBrowsingContext context)
        {
            var configuration = context.Configuration;
            var creator = configuration.GetService<Func<IBrowsingContext, TService>>();
            return creator != null ? creator.Invoke(context) : default(TService);
        }
    }
}
