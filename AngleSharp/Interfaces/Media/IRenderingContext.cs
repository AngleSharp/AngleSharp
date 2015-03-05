namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Html;
    using System;
    using System.IO;


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

        /// <summary>
        /// Converts the current data to the given image format.
        /// </summary>
        /// <param name="type">The type of the image format.</param>
        /// <returns>The raw content bytes of the image.</returns>
        Byte[] ToImage(String type);
    }
}
