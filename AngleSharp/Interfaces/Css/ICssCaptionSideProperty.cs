namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents the CSS caption-side property.
    /// </summary>
    public interface ICssCaptionSideProperty : ICssProperty
    {
        /// <summary>
        /// Gets if the caption box will be above the table.
        /// </summary>
        Boolean IsOnTop { get; }
    }
}
