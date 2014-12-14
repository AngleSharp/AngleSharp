namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css.Values;

    /// <summary>
    /// Represents the CSS clip property.
    /// </summary>
    public interface ICssClipProperty : ICssProperty
    {
        /// <summary>
        /// Gets the shape of the selected clipping region.
        /// If this value is null, then the clipping is
        /// determined automatically.
        /// </summary>
        Shape Clip { get; }
    }
}
