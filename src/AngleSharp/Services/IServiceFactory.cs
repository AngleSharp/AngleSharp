namespace AngleSharp.Services
{
    /// <summary>
    /// Represents the factory to create arbitrary (third-party) services.
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// Creates a new service for the given context.
        /// </summary>
        /// <typeparam name="TService">The type of service.</typeparam>
        /// <param name="context">The context to host the service.</param>
        /// <returns>The created service or a default instance.</returns>
        TService Create<TService>(IBrowsingContext context);
    }
}
