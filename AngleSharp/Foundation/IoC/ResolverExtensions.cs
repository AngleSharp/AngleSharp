namespace AngleSharp
{
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Some useful extensions for the dependency resolver interface.
    /// </summary>
    static class ResolverExtensions
    {
        /// <summary>
        /// Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <typeparam name="TService">The type of the requested service or object.</typeparam>
        /// <param name="resolver">The dependency resolver instance that this method extends.</param>
        /// <returns>The requested service or object.</returns>
        public static TService GetService<TService>(this IDependencyResolver resolver)
        {
            return (TService)resolver.GetService(typeof(TService));
        }

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <typeparam name="TService">The type of the requested service or object.</typeparam>
        /// <param name="resolver">The dependency resolver instance that this method extends.</param>
        /// <returns>The requested services.</returns>
        public static IEnumerable<TService> GetServices<TService>(this IDependencyResolver resolver)
        {
            return resolver.GetServices(typeof(TService)).Cast<TService>();
        }
    }
}
