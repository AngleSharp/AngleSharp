namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Media;
    using AngleSharp.Extensions;
    using System;
    using System.IO;

    /// <summary>
    /// Represents the HTML canvas element.
    /// See: http://www.whatwg.org/specs/web-apps/current-work/multipage/the-canvas-element.html
    /// Alternative: http://www.w3.org/html/wg/drafts/html/master/embedded-content-0.html#the-canvas-element
    /// </summary>
    sealed class HTMLCanvasElement : HTMLElement, IHtmlCanvasElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML canvas element.
        /// </summary>
        internal HTMLCanvasElement()
            : base(Tags.Canvas)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the drawing context.
        /// </summary>
        /// <param name="contextId">A context id like 2d.</param>
        /// <returns>An object that defines the drawing context.</returns>
        public IRenderingContext GetContext(String contextId)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Gets an indicator if a context with the given parameters could be created.
        /// </summary>
        /// <param name="contextId">A context id like 2d.</param>
        /// <returns>True if the context is supported, otherwise false.</returns>
        public Boolean IsSupportingContext(String contextId)
        {
            //TODO
            return false;
        }

        /// <summary>
        /// Changes the context the element is related to the given one.
        /// </summary>
        /// <param name="context">The new context.</param>
        public void SetContext(IRenderingContext context)
        {
            //TODO
        }

        /// <summary>
        /// Returns a Data URI with the bitmap data of the context.
        /// </summary>
        /// <param name="type">The type of image e.g image/png.</param>
        /// <returns>A data URI with the data if any.</returns>
        public String ToDataUrl(String type = null)
        {
            //TODO
            return String.Empty;
        }

        /// <summary>
        /// Creates a BLOB out of the canvas pixel data and passes it
        /// to the given callback.
        /// </summary>
        /// <param name="callback">The callback function.</param>
        /// <param name="type">The type of object to create.</param>
        public void ToBlob(Action<Stream> callback, String type = null)
        {
            //TODO
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the displayed width of the canvas element.
        /// </summary>
        public Int32 Width
        {
            get { return GetAttribute(AttributeNames.Width).ToInteger(300); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the canvas element.
        /// </summary>
        public Int32 Height
        {
            get { return GetAttribute(AttributeNames.Height).ToInteger(150); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
        }

        #endregion
    }
}
