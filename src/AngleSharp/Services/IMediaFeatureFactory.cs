namespace AngleSharp.Services
{
    using AngleSharp.Dom.Css;
    using System;

    /// <summary>
    /// Represents the factory to create media features.
    /// </summary>
    public interface IMediaFeatureFactory
    {
        /// <summary>
        /// Creates a media feature with the given name.
        /// </summary>
        /// <param name="name">The name of the feature.</param>
        /// <returns>The new feature, if any.</returns>
        IMediaFeature Create(String name);
    }
}
