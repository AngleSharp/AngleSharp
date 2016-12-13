namespace AngleSharp.Css
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the API of an available engine for computing the stylesheet.
    /// </summary>
    public interface IStylingService
    {
        /// <summary>
        /// Checks if the given type is supported.
        /// </summary>
        /// <param name="mimeType">The type of the style.</param>
        /// <returns>True if the type is supported, otherwise false.</returns>
        Boolean SupportsType(String mimeType);

        /// <summary>
        /// Parses a style sheet for the given response asynchronously.
        /// </summary>
        /// <param name="response">
        /// The response with the stream representing the source of the
        /// stylesheet.
        /// </param>
        /// <param name="options">
        /// The options with the parameters for evaluating the style.
        /// </param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task resulting in the style sheet.</returns>
        Task<IStyleSheet> ParseStylesheetAsync(IResponse response, StyleOptions options, CancellationToken cancel);
    }
}
