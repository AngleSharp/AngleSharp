namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the CSS z-index property.
    /// </summary>
    public interface ICssZIndexProperty : ICssProperty
    {
        /// <summary>
        /// Gets if the z-index has been set at all.
        /// </summary>
        Boolean HasIndex { get; }

        /// <summary>
        /// Gets the index in the stacking order, if any.
        /// </summary>
        Int32 Index { get; }
    }
}
