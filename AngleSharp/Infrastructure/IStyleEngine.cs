namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM;
    using System;
    using System.IO;

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
        /// <param name="owner">The owner of the style sheet, if any.</param>
        /// <returns>The created style sheet.</returns>
        IStyleSheet CreateStyleSheetFor(String source, IElement owner);

        /// <summary>
        /// Creates a style sheet for the given stream.
        /// </summary>
        /// <param name="source">The stream with the source describing the style sheet.</param>
        /// <param name="owner">The owner of the style sheet, if any.</param>
        /// <returns>The created style sheet.</returns>
        IStyleSheet CreateStyleSheetFor(Stream source, IElement owner);
    }
}
