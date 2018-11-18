namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the area HTML element.
    /// </summary>
    [DomName("HTMLAreaElement")]
    public interface IHtmlAreaElement : IHtmlUrlBaseElement
    {
        /// <summary>
        /// Gets or sets the alternative text for the element.
        /// </summary>
        [DomName("alt")]
        String AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the coordinates to define the hot-spot region.
        /// </summary>
        [DomName("coords")]
        String Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the shape of the hot-spot, limited to known values.
        /// </summary>
        [DomName("shape")]
        String Shape { get; set; }
        
    }
}
