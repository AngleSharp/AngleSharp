namespace AngleSharp.Services
{
    using AngleSharp.Services.Scripting;
    using System;

    /// <summary>
    /// Defines the methods to access available script engines.
    /// </summary>
    public interface IScriptingProvider
    {
        /// <summary>
        /// Gets the registered engine for the provided mime-type.
        /// </summary>
        /// <param name="mimeType">The type of the engine.</param>
        /// <returns>The engine for the mime-type, if any.</returns>
        IScriptEngine GetEngine(String mimeType);
    }
}
