namespace AngleSharp.Services.Scripting
{
    using AngleSharp.Network;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the API of an available engine for running scripts provided in
    /// the code.
    /// </summary>
    public interface IScriptEngine
    {
        /// <summary>
        /// The type of the scripting language.
        /// </summary>
        String Type { get; }

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
