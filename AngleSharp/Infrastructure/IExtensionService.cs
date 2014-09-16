using System;
namespace AngleSharp.Infrastructure
{
    /// <summary>
    /// Represents an extension offering a special kind of (DOM) interface.
    /// </summary>
    /// <typeparam name="TInterface">The interface to offer.</typeparam>
    public interface IExtensionService<TInterface> : IService
    {
        /// <summary>
        /// Gets the interface specialized for the given address.
        /// </summary>
        /// <param name="origin">The origin of the requesting page.</param>
        /// <returns>The instance.</returns>
        TInterface this[String origin] { get; }
    }
}
