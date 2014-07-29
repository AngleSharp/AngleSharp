namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the image element.
    /// </summary>
    sealed class HTMLImageElement : HTMLElement, IHtmlImageElement
    {
        #region Fields

        Int32 _imageWidth;
        Int32 _imageHeight;
        Boolean _loaded;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new image element.
        /// </summary>
        internal HTMLImageElement()
            : base(Tags.Img, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            _loaded = true;

            //TODO
            _imageHeight = 0;
            _imageWidth = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        public String Source
        {
            get { return GetAttribute(AttributeNames.Src); }
            set { SetAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        public String AlternativeText
        {
            get { return GetAttribute(AttributeNames.Alt); }
            set { SetAttribute(AttributeNames.Alt, value); }
        }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        public String CrossOrigin
        {
            get { return GetAttribute(AttributeNames.CrossOrigin); }
            set { SetAttribute(AttributeNames.CrossOrigin, value); }
        }

        /// <summary>
        /// Gets or sets the usemap attribute, which indicates that the image has an associated image map.
        /// </summary>
        public String UseMap
        {
            get { return GetAttribute(AttributeNames.UseMap); }
            set { SetAttribute(AttributeNames.UseMap, value); }
        }

        /// <summary>
        /// Gets or sets the displayed width of the image element.
        /// </summary>
        public Int32 DisplayWidth
        {
            get { return GetAttribute(AttributeNames.Width).ToInteger(_imageWidth); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the image element.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetAttribute(AttributeNames.Height).ToInteger(_imageHeight); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public Int32 OriginalWidth
        {
            get { return _imageWidth; }
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public Int32 OriginalHeight
        {
            get { return _imageHeight; }
        }

        /// <summary>
        /// Gets if the image is completely available.
        /// </summary>
        public Boolean IsCompleted
        {
            get { return _loaded; }
        }

        /// <summary>
        /// Gets or sets if the image element is a map.
        /// The attribute must not be specified on an element that does not
        /// have an ancestor a element with an href attribute.
        /// </summary>
        public Boolean IsMap
        {
            get { return GetAttribute(AttributeNames.IsMap) != null; }
            set { SetAttribute(AttributeNames.IsMap, value ? String.Empty : null); }
        }

        #endregion
    }
}
