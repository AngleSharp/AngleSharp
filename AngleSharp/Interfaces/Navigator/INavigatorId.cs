namespace AngleSharp.Dom.Navigator
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Holds the user-agent information.
    /// </summary>
    [DomName("NavigatorID")]
    [DomNoInterfaceObject]
    public interface INavigatorId
    {
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        [DomName("appName")]
        String Name { get; }

        /// <summary>
        /// Gets the version of the application.
        /// </summary>
        [DomName("appVersion")]
        String Version { get; }

        /// <summary>
        /// Gets the platform of the application.
        /// </summary>
        [DomName("platform")]
        String Platform { get; }

        /// <summary>
        /// Gets the full name of the user-agent.
        /// </summary>
        [DomName("userAgent")]
        String UserAgent { get; }
    }
}
