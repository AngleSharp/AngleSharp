namespace AngleSharp
{
    using AngleSharp.DOM;
    using System;

    /// <summary>
    /// Extensions for the objects of type Location.
    /// </summary>
    static class LocationExtensions
    {
        /// <summary>
        /// Converts the given location to a URI.
        /// </summary>
        /// <param name="location">The location to convert.</param>
        /// <returns>The uri.</returns>
        public static Uri ToUri(this ILocation location)
        {
            return new Uri(location.Href);
        }
    }
}
