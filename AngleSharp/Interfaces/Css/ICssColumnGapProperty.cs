namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Represents the CSS column-gap property.
    /// </summary>
    public interface ICssColumnGapProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected width of gaps between columns.
        /// </summary>
        Length Gap { get; }
    }
}
