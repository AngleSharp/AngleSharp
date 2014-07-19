namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM;
    using System;

    /// <summary>
    /// Defines the API of an available engine for computing
    /// the stylesheet.
    /// </summary>
    public interface IStyleEngine
    {
        /// <summary>
        /// The type of the styling set.
        /// </summary>
        String Type { get; }

        /// <summary>
        /// Creates a style sheet for the given source.
        /// </summary>
        /// <param name="source">The source code describing the style sheet.</param>
        /// <returns>The created style sheet.</returns>
        StyleSheet CreateStyleSheetFor(String source);
    }
}
