namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the col HTML element.
    /// </summary>
    [DomName("HTMLTableColElement")]
    public interface IHtmlTableColumnElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the number of columns in a group or affected by a grouping.
        /// </summary>
        [DomName("span")]
        UInt32 Span { get; set; }
    }
}
