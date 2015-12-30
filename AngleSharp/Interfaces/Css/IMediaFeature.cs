namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents a CSS media feature.
    /// </summary>
    public interface IMediaFeature : ICssNode
    {
        /// <summary>
        /// Gets the name of the feature.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Gets if the feature represents the minimum.
        /// </summary>
        Boolean IsMinimum { get; }

        /// <summary>
        /// Gets if the feature represents the maximum.
        /// </summary>
        Boolean IsMaximum { get; }

        /// <summary>
        /// Gets the value of the feature, if any.
        /// </summary>
        String Value { get; }

        /// <summary>
        /// Gets if a value has been set for this feature.
        /// </summary>
        Boolean HasValue { get; }
    }
}
