namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the progress HTML element.
    /// </summary>
    [DomName("HTMLProgressElement")]
    public interface IHtmlProgressElement : IHtmlElement
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
        Double Max { get; set; }

        /// <summary>
        /// Gets the position.
        /// </summary>
        [DomName("position")]
        Double Position { get; }

        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        [DomName("labels")]
        INodeList Labels { get; }
    }
}
