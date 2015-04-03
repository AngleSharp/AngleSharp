namespace AngleSharp.Css
{
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using System;

    /// <summary>
    /// Defines the API of an available engine for computing the stylesheet.
    /// </summary>
    public interface IStyleEngine
    {
        /// <summary>
        /// The type of the styling set.
        /// </summary>
        String Type { get; }

        /// <summary>
        /// Gets the default stylesheet, if any.
        /// </summary>
        IStyleSheet Default { get; }

        /// <summary>
        /// Creates a style sheet for the given source.
        /// </summary>
        /// <param name="source">
        /// The source code describing the style sheet.
        /// </param>
        /// <param name="options">
        /// The options with the parameters for evaluating the style.
        /// </param>
        /// <returns>The created style sheet.</returns>
        IStyleSheet Parse(String source, StyleOptions options);

        /// <summary>
        /// Creates a style sheet for the given response from a request.
        /// </summary>
        /// <param name="response">
        /// The response with the stream representing the source of the
        /// stylesheet.
        /// </param>
        /// <param name="options">
        /// The options with the parameters for evaluating the style.
        /// </param>
        /// <returns>The created style sheet.</returns>
        IStyleSheet Parse(IResponse response, StyleOptions options);
    }
}
