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
        /// Evaluates the given source in the specified context.
        /// </summary>
        /// <param name="source">The source code of the script.</param>
        /// <param name="context">The context in which the script should be invoked.</param>
        void Evaluate(String source, IWindow context);

        /// <summary>
        /// Evaluates the given stream in the specified context.
        /// </summary>
        /// <param name="source">The stream with the source of the script.</param>
        /// <param name="context">The context in which the script should be invoked.</param>
        void Evaluate(Stream source, IWindow context);
    }
}
