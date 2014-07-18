namespace AngleSharp.Infrastructure
{
    using System;

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
    }
}
