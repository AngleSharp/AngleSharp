namespace AngleSharp.Services.Styling
{
    using AngleSharp.Dom.Css;
    using System;

    /// <summary>
    /// Specializes the API for a CSS style engine.
    /// </summary>
    public interface ICssStyleEngine : IStyleEngine
    {
        /// <summary>
        /// Gets the default CSS stylesheet.
        /// </summary>
        ICssStyleSheet Default { get; }

        /// <summary>
        /// Creates a style declaration for the given source.
        /// </summary>
        /// <param name="source">
        /// The source code for the inline style declaration.
        /// </param>
        /// <param name="options">
        /// The options with the parameters for evaluating the style.
        /// </param>
        /// <returns>The created style declaration.</returns>
        ICssStyleDeclaration ParseDeclaration(String source, StyleOptions options);

        /// <summary>
        /// Creates a media list for the given source.
        /// </summary>
        /// <param name="source">The media source.</param>
        /// <param name="options">
        /// The options with the parameters for evaluating the style.
        /// </param>
        /// <returns>The created media list.</returns>
        IMediaList ParseMedia(String source, StyleOptions options);
    }
}
