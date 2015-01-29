namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents the CSS column-span property.
    /// </summary>
    public interface ICssColumnSpanProperty : ICssProperty
    {
        /// <summary>
        /// Gets if the element should span across all columns.
        /// </summary>
        Boolean IsSpanning { get; }
    }
}
