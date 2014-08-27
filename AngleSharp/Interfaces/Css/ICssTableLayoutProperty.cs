namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the CSS table-layout property.
    /// </summary>
    public interface ICssTableLayoutProperty : ICssProperty
    {
        /// <summary>
        /// Gets if table and column widths are set by the widths of table and
        /// col elements or by the width of the first row of cells.
        /// </summary>
        Boolean IsFixed { get; }
    }
}
