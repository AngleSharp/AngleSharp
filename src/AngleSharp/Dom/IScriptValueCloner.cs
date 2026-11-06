namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Implemented by a scripting engine to tell what a value of script is without it: what keeps
    /// such a value for later (e.g. IndexedDB) can't hold on to the engine it comes from.
    /// </summary>
    public interface IScriptValueCloner
    {
        /// <summary>
        /// Gets the value standing for undefined, which script can tell apart from null.
        /// </summary>
        Object? Undefined { get; }

        /// <summary>
        /// Gets the structured clone of the value in terms of .NET, which the engine gets back
        /// as a new value of script: null, strings, booleans, doubles, dates, bytes, arrays of
        /// objects and dictionaries with strings as keys.
        /// </summary>
        /// <param name="value">The value given by script.</param>
        /// <returns>The clone of the value.</returns>
        /// <exception cref="DomException">The value can't be cloned (DataCloneError).</exception>
        Object? Clone(Object? value);
    }
}
