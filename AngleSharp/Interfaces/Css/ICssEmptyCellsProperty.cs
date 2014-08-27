namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the CSS empty-cells property.
    /// </summary>
    public interface ICssEmptyCellsProperty : ICssProperty
    {
        /// <summary>
        /// Gets if borders and backgrounds should be drawn like
        /// in a normal cells.
        /// </summary>
        Boolean IsVisible { get; }
    }
}
