﻿namespace AngleSharp.Html.Dom
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Media.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents the HTML canvas element.
    /// See: http://www.whatwg.org/specs/web-apps/current-work/multipage/the-canvas-element.html
    /// Alternative: http://www.w3.org/html/wg/drafts/html/master/embedded-content-0.html#the-canvas-element
    /// </summary>
    sealed class HtmlCanvasElement : HtmlElement, IHtmlCanvasElement
    {
        #region Fields

        private readonly IEnumerable<IRenderingService> _renderServices;
        private ContextMode _mode;
        private IRenderingContext _current;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML canvas element.
        /// </summary>
        public HtmlCanvasElement(Document owner, String prefix = null)
            : base(owner, TagNames.Canvas, prefix)
        {
            _renderServices = owner.Context.GetServices<IRenderingService>();
            _mode = ContextMode.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the displayed width of the canvas element.
        /// </summary>
        public Int32 Width
        {
            get { return this.GetOwnAttribute(AttributeNames.Width).ToInteger(300); }
            set { this.SetOwnAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the canvas element.
        /// </summary>
        public Int32 Height
        {
            get { return this.GetOwnAttribute(AttributeNames.Height).ToInteger(150); }
            set { this.SetOwnAttribute(AttributeNames.Height, value.ToString()); }
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
            if (_current == null || contextId.Isi(_current.ContextId))
            {
                foreach (var renderService in _renderServices)
                {
                    if (renderService.IsSupportingContext(contextId))
                    {
                        var context = renderService.CreateContext(this, contextId);

                        if (context != null)
                        {
                            _mode = GetModeFrom(contextId);
                            _current = context;
                        }

                        return context;
                    }
                }

                return null;
            }

            return _current;

        }

        /// <summary>
        /// Gets an indicator if a context with the given parameters could be created.
        /// </summary>
        /// <param name="contextId">A context id like 2d.</param>
        /// <returns>True if the context is supported, otherwise false.</returns>
        public Boolean IsSupportingContext(String contextId)
        {
            foreach (var renderService in _renderServices)
            {
                if (renderService.IsSupportingContext(contextId))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Changes the context the element is related to the given one.
        /// </summary>
        /// <param name="context">The new context.</param>
        public void SetContext(IRenderingContext context)
        {
            if (_mode != ContextMode.None && _mode != ContextMode.Indirect)
                throw new DomException(DomError.InvalidState);

            if (context.IsFixed)
                throw new DomException(DomError.InvalidState);

            if (context.Host != this)
                throw new DomException(DomError.InUse);

            _current = context;
            _mode = ContextMode.Indirect;
        }

        /// <summary>
        /// Returns a Data URI with the bitmap data of the context.
        /// </summary>
        /// <param name="type">The type of image e.g image/png.</param>
        /// <returns>A data URI with the data if any.</returns>
        public String ToDataUrl(String type = null)
        {
            var content = GetImageData(type);
            return Convert.ToBase64String(content);
        }

        /// <summary>
        /// Creates a BLOB out of the canvas pixel data and passes it
        /// to the given callback.
        /// </summary>
        /// <param name="callback">The callback function.</param>
        /// <param name="type">The type of object to create.</param>
        public void ToBlob(Action<Stream> callback, String type = null)
        {
            var content = GetImageData(type);
            var ms = new MemoryStream(content);
            callback(ms);
        }

        #endregion

        #region Helpers

        private Byte[] GetImageData(String type)
        {
            return _current?.ToImage(type ?? MimeTypeNames.Plain) ?? new Byte[0];
        }

        private static ContextMode GetModeFrom(String contextId)
        {
            if (contextId.Isi(Keywords.TwoD))
            {
                return ContextMode.Direct2d;
            }
            else if (contextId.Isi(Keywords.WebGl))
            {
                return ContextMode.DirectWebGl;
            }

            return ContextMode.None;
        }

        #endregion

        #region Context Mode

        private enum ContextMode : byte
        {
            None,
            Direct2d, 
            DirectWebGl,
            Indirect,
            Proxied
        }

        #endregion
    }
}
