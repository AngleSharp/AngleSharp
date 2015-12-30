namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents a function of the @document rule.
    /// </summary>
    public interface IDocumentFunction : ICssNode
    {
        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Gets the function's data (argument).
        /// </summary>
        String Data { get; }

        /// <summary>
        /// Evaluates the function for the provided URL.
        /// </summary>
        /// <param name="url">The URL to evaluate.</param>
        /// <returns>True if the URL is matched, otherwise false.</returns>
        Boolean Matches(Url url);
    }
}
