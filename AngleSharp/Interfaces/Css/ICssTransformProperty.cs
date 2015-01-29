namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css.Values;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS transform property.
    /// </summary>
    public interface ICssTransformProperty : ICssProperty
    {
        /// <summary>
        /// Gets the enumeration over all transformations.
        /// </summary>
        IEnumerable<ITransform> Transforms { get; }
    }
}
