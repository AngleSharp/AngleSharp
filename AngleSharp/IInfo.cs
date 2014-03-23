namespace AngleSharp
{
    using System;

    /// <summary>
    /// Represents some basic information about the AngleSharp library.
    /// </summary>
    public interface IInfo
    {
        /// <summary>
        /// Gets the current version of AngleSharp.
        /// </summary>
        String Version { get; }

        /// <summary>
        /// Gets the transmitted default user-agent identifier.
        /// </summary>
        String Agent { get; }
    }
}
