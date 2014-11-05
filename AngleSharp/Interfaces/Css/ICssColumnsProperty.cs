namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Represents the CSS column-count property.
    /// </summary>
    public interface ICssColumnCountProperty : ICssProperty
    {
        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        Int32? Count { get; }
    }

    /// <summary>
    /// Represents the CSS column-width property.
    /// </summary>
    public interface ICssColumnWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets the width of a single columns.
        /// </summary>
        Length? Width { get; }
    }

    /// <summary>
    /// Represents the CSS columns shorthand property.
    /// </summary>
    public interface ICssColumnsProperty : ICssProperty, ICssColumnWidthProperty, ICssColumnCountProperty
    {
    }
}
