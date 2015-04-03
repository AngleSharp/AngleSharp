namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Events;
    using AngleSharp.Services;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Represents the interface for a general setup of AngleSharp
    /// or a particular AngleSharp request.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets or sets the current CSS mode. If this is set to true,
        /// then style-sheets will be loaded, parsed and evaluated.
        /// </summary>
        Boolean IsStyling { get; set; }

        /// <summary>
        /// Gets or sets the culture to use.
        /// </summary>
        CultureInfo Culture { get; set; }

        /// <summary>
        /// Gets an enumeration over the available style engines,
        /// besides the default CSS engine.
        /// </summary>
        IEnumerable<IStyleEngine> StyleEngines { get; }

        /// <summary>
        /// Gets an enumeration over the available services.
        /// </summary>
        IEnumerable<IService> Services { get; }

        /// <summary>
        /// Gets the assigned event aggregator.
        /// </summary>
        IEventAggregator Events { get; }
    }
}
