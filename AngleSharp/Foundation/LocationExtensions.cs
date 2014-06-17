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
        
        /// <summary>
        /// Returns the CSS representation of the given URL.
        /// </summary>
        /// <param name="location">The location to represent.</param>
        /// <returns>The CSS value string.</returns>
        public static String ToCss(this ILocation location)
        {
            return FunctionNames.Build(FunctionNames.Url, String.Concat("'", location.ToString(), "'"));
        }
    }
}
