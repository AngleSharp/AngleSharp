namespace AngleSharp.Infrastructure
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the API of an available engine for running
    /// scripts provided in the code.
    /// </summary>
    public interface IScriptEngine
    {
        /// <summary>
        /// The type of the scripting language.
        /// </summary>
        String Type { get; }

        /// <summary>
        /// Evaluates the given source with the specified options.
        /// </summary>
        /// <param name="source">The source code of the script.</param>
        /// <param name="options">The options with the parameters for invoking the script.</param>
        void Evaluate(String source, ScriptOptions options);

        /// <summary>
        /// Evaluates the given stream with the specified options.
        /// </summary>
        /// <param name="source">The stream with the source of the script.</param>
        /// <param name="options">The options with the parameters for invoking the script.</param>
        void Evaluate(Stream source, ScriptOptions options);
    }
}
