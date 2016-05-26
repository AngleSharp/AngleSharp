namespace AngleSharp
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the interface for a general setup of AngleSharp
    /// or a particular AngleSharp request.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets an enumeration over the available services.
        /// </summary>
        IEnumerable<Object> Services { get; }
    }
}
