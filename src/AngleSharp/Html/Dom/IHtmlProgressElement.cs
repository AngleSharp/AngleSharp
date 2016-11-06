namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the progress HTML element.
    /// </summary>
    [DomName("HTMLProgressElement")]
    public interface IHtmlProgressElement : IHtmlElement, ILabelabelElement
    {
        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        [DomName("value")]
        Double Value { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        [DomName("max")]
        Double Maximum { get; set; }

        /// <summary>
        /// Gets the position.
        /// </summary>
        [DomName("position")]
        Double Position { get; }
    }
}
