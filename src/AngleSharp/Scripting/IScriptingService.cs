namespace AngleSharp.Scripting
{
    using AngleSharp.Io;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the API of an available engine for running scripts provided in
    /// the code.
    /// </summary>
    public interface IScriptingService
    {
        /// <summary>
        /// Checks if the given type is supported.
        /// </summary>
        /// <param name="mimeType">The type of the script.</param>
        /// <returns>True if the type is supported, otherwise false.</returns>
        Boolean SupportsType(String mimeType);

        /// <summary>
        /// Evaluates a script for the given response asynchronously.
        /// </summary>
        /// <param name="response">
        /// The response with the stream representing the source of the script.
        /// </param>
        /// <param name="options">
        /// The options with the parameters for invoking the script.
        /// </param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task evaluating the script.</returns>
        Task EvaluateScriptAsync(IResponse response, ScriptOptions options, CancellationToken cancel);
    }
}
