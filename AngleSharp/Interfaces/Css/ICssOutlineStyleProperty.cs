namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Repesents the CSS outline-style property.
    /// </summary>
    public interface ICssOutlineStyleProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected outline style.
        /// </summary>
        LineStyle Style { get; }
    }
}
