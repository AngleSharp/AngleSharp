namespace AngleSharp.Infrastructure
{
    using System;

    /// <summary>
    /// Defines the API of an available engine for computing
    /// the stylesheet.
    /// </summary>
    public interface IStyleEngine
    {
        /// <summary>
        /// The type of the styling set.
        /// </summary>
        String Type { get; }
    }
}
