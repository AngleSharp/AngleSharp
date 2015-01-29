namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents the CSS z-index property.
    /// </summary>
    public interface ICssZIndexProperty : ICssProperty
    {
        /// <summary>
        /// Gets the index in the stacking order, if any.
        /// </summary>
        Int32? Index { get; }
    }
}
