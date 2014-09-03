namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the CSS column-count property.
    /// </summary>
    public interface ICssColumnCountProperty : ICssProperty
    {
        /// <summary>
        /// Gets if the column count should be considered.
        /// </summary>
        Boolean IsUsed { get; }

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        Int32 Count { get; }
    }

    /// <summary>
    /// Represents the CSS column-width property.
    /// </summary>
    public interface ICssColumnWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets if the column width should be considered.
        /// </summary>
        Boolean IsUsed { get; }

        /// <summary>
        /// Gets the width of a single columns.
        /// </summary>
        Length Width { get; }
    }

    /// <summary>
    /// Represents the CSS columns shorthand property.
    /// </summary>
    public interface ICssColumnsProperty : ICssProperty, ICssColumnWidthProperty, ICssColumnCountProperty
    {
    }
}
