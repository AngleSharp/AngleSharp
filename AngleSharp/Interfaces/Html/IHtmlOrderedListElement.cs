namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the ol HTML element.
    /// </summary>
    [DomName("HTMLOListElement")]
    public interface IHtmlOrderedListElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets if the order is reversed.
        /// </summary>
        [DomName("reversed")]
        Boolean IsReversed { get; set; }

        /// <summary>
        /// Gets or sets the lowest number.
        /// </summary>
        [DomName("start")]
        Int32 Start { get; set; }

        /// <summary>
        /// Gets or sets the type of enumeration.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }
    }
}
