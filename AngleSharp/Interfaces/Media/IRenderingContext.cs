namespace AngleSharp.DOM.Media
{
    using AngleSharp.Attributes;
using AngleSharp.DOM.Html;
using System;

    /// <summary>
    /// Represents the typedef for any rendering context.
    /// This is shown is the base interface for all rendering
    /// contexts.
    /// </summary>
    [DomName("RenderingContext")]
    public interface IRenderingContext
    {
        /// <summary>
        /// Gets the ID of the rendering context.
        /// </summary>
        String ContextId { get; }

        /// <summary>
        /// Gets if the context's bitmap mode is fixed.
        /// </summary>
        Boolean IsFixed { get; }

        /// <summary>
        /// Gets or sets the bound host of the context.
        /// </summary>
        IHtmlCanvasElement Host { get; set; }
    }
}
