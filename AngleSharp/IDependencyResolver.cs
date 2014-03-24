namespace AngleSharp
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the methods that simplify service location and dependency resolution.
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <param name="requestedService">The type of the requested service or object.</param>
        /// <returns>The requested service or object.</returns>
        Object GetService(Type requestedService);

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <param name="requestedService">The type of the requested service or object.</param>
        /// <returns>The requested service or object.</returns>
        IEnumerable<Object> GetServices(Type requestedService);
    }
}
