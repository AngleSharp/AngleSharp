namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents the CSS transform-style property.
    /// </summary>
    public interface ICssTransformStyleProperty : ICssProperty
    {
        /// <summary>
        /// Gets if the children of the element are lying in the plane of
        /// the element itself.
        /// </summary>
        Boolean IsFlat { get; }
    }
}
