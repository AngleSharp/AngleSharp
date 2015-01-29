namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the meter HTML element.
    /// </summary>
    [DomName("HTMLMeterElement")]
    public interface IHtmlMeterElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        [DomName("value")]
        Double Value { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        [DomName("min")]
        Double Minimum { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        [DomName("max")]
        Double Maximum { get; set; }

        /// <summary>
        /// Gets or sets the low value.
        /// </summary>
        [DomName("low")]
        Double Low { get; set; }

        /// <summary>
        /// Gets or sets the high value.
        /// </summary>
        [DomName("high")]
        Double High { get; set; }

        /// <summary>
        /// Gets or sets the optimum value.
        /// </summary>
        [DomName("optimum")]
        Double Optimum { get; set; }

        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        [DomName("labels")]
        INodeList Labels { get; }
    }
}
