namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the image element.
    /// </summary>
    sealed class HTMLImageElement : HTMLElement, IHtmlImageElement
    {
        #region Fields

        UInt32 _imageWidth;
        UInt32 _imageHeight;
        Boolean _loaded;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new image element.
        /// </summary>
        internal HTMLImageElement()
        {
            _loaded = true;
            _name = Tags.Image;

            //TODO
            _imageHeight = 0;
            _imageWidth = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        public String Src
        {
            get { return GetAttribute(AttributeNames.Src); }
            set { SetAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        public String Alt
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
        public UInt32 Width
        {
            get { return ToInteger(GetAttribute(AttributeNames.Width), _imageWidth); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the image element.
        /// </summary>
        public UInt32 Height
        {
            get { return ToInteger(GetAttribute(AttributeNames.Height), _imageHeight); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public UInt32 NaturalWidth
        {
            get { return _imageWidth; }
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public UInt32 NaturalHeight
        {
            get { return _imageHeight; }
        }

        /// <summary>
        /// Gets if the image is completely available.
        /// </summary>
        public Boolean Complete
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

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
