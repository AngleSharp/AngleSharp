namespace AngleSharp.DOM.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Media;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the image element.
    /// </summary>
    sealed class HTMLImageElement : HTMLElement, IHtmlImageElement
    {
        #region Fields

        Task<IImageInfo> _imageTask;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new image element.
        /// </summary>
        internal HTMLImageElement()
            : base(Tags.Img, NodeFlags.Special | NodeFlags.SelfClosing)
        {
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
            get { return GetAttribute(AttributeNames.Width).ToInteger(OriginalWidth); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the image element.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetAttribute(AttributeNames.Height).ToInteger(OriginalHeight); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public Int32 OriginalWidth
        {
            get { return _imageTask != null ? (_imageTask.IsCompleted && _imageTask.Result != null ? _imageTask.Result.Width : 0) : 0; }
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public Int32 OriginalHeight
        {
            get { return _imageTask != null ? (_imageTask.IsCompleted && _imageTask.Result != null ? _imageTask.Result.Height : 0) : 0; }
        }

        /// <summary>
        /// Gets if the image is completely available.
        /// </summary>
        public Boolean IsCompleted
        {
            get { return _imageTask == null || _imageTask.IsCompleted; }
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

        #region Methods

        internal override void Close()
        {
            base.Close();
            var src = Source;

            if (src != null)
            {
                var url = HyperRef(src);
                _imageTask = Owner.Options.LoadResource<IImageInfo>(url);
                _imageTask.ContinueWith(task => this.FireSimpleEvent(EventNames.Load));
            }        
        }

        #endregion
    }
}
