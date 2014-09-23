namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS text-indent property.
    /// </summary>
    public interface ICssTextIndentProperty : ICssProperty
    {
        /// <summary>
        /// Gets the indentation, which is either a percentage of the containing block width
        /// or specified as fixed length. Negative values are allowed.
        /// </summary>
        IDistance Indent { get; }
    }
}
