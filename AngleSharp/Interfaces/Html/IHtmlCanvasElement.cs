namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the canvas HTML element.
    /// </summary>
    [DomName("HTMLCanvasElement")]
    public interface IHtmlCanvasElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the displayed width of the canvas element.
        /// </summary>
        [DomName("width")]
        Int32 Width { get; set; }

        /// <summary>
        /// Gets or sets the displayed height of the canvas element.
        /// </summary>
        [DomName("height")]
        Int32 Height { get; set; }

        /// <summary>
        /// Returns a Data URI with the bitmap data of the context.
        /// </summary>
        /// <param name="type">The type of image e.g image/png.</param>
        /// <returns>A data URI with the data if any.</returns>
        [DomName("toDataURL")]
        String ToDataURL(String type = null);

        /// <summary>
        /// Creates a BLOB out of the canvas pixel data and passes it
        /// to the given callback.
        /// </summary>
        /// <param name="callback">The callback function.</param>
        /// <param name="type">The type of object to create.</param>
        [DomName("toBlob")]
        void ToBlob(Action<Object> callback, String type = null);

        /// <summary>
        /// Gets the drawing context.
        /// </summary>
        /// <param name="contextId">A context id like 2d.</param>
        /// <returns>An object that defines the drawing context.</returns>
        [DomName("getContext")]
        RenderingContext GetContext(String contextId);
    }
}
