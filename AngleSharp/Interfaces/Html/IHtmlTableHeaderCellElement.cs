namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the th HTML element.
    /// </summary>
    [DomName("HTMLTableHeaderCellElement")]
    public interface IHtmlTableHeaderCellElement : IHtmlTableCellElement
    {
        /// <summary>
        /// Gets or sets the scope of the th element.
        /// </summary>
        [DomName("scope")]
        String Scope { get; set; }
    }
}
