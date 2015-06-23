namespace AngleSharp.Dom.Html
{
    using System;
    using System.Threading.Tasks;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services.Media;

    /// <summary>
    /// Represents the image element.
    /// </summary>
    sealed class HtmlImageElement : HtmlElement, IHtmlImageElement
    {
        #region Fields

        readonly BoundLocation _src;
        Task<IImageInfo> _current;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new image element.
        /// </summary>
        public HtmlImageElement(Document owner, String prefix = null)
            : base(owner, Tags.Img, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            _src = new BoundLocation(this, AttributeNames.Src);
            RegisterAttributeObserver(AttributeNames.Src, UpdateSource);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the actual used image source.
        /// </summary>
        public String ActualSource
        {
            get { return Source;  }
        }

        /// <summary>
        /// Gets or sets the image candidates for higher density images.
        /// </summary>
        public String SourceSet
        {
            get { return GetOwnAttribute(AttributeNames.SrcSet); }
            set { SetOwnAttribute(AttributeNames.SrcSet, value); }
        }

        /// <summary>
        /// Gets or sets the sizes to responsively.
        /// </summary>
        public String Sizes
        {
            get { return GetOwnAttribute(AttributeNames.Sizes); }
            set { SetOwnAttribute(AttributeNames.Sizes, value); }
        }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        public String Source
        {
            get { return _src.Href; }
            set { _src.Href = value; }
        }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        public String AlternativeText
        {
            get { return GetOwnAttribute(AttributeNames.Alt); }
            set { SetOwnAttribute(AttributeNames.Alt, value); }
        }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        public String CrossOrigin
        {
            get { return GetOwnAttribute(AttributeNames.CrossOrigin); }
            set { SetOwnAttribute(AttributeNames.CrossOrigin, value); }
        }

        /// <summary>
        /// Gets or sets the usemap attribute, which indicates that the image
        /// has an associated image map.
        /// </summary>
        public String UseMap
        {
            get { return GetOwnAttribute(AttributeNames.UseMap); }
            set { SetOwnAttribute(AttributeNames.UseMap, value); }
        }

        /// <summary>
        /// Gets or sets the displayed width of the image element.
        /// </summary>
        public Int32 DisplayWidth
        {
            get { return GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth); }
            set { SetOwnAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the image element.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight); }
            set { SetOwnAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public Int32 OriginalWidth
        {
            get { return _current != null ? (_current.IsCompleted && _current.Result != null ? _current.Result.Width : 0) : 0; }
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public Int32 OriginalHeight
        {
            get { return _current != null ? (_current.IsCompleted && _current.Result != null ? _current.Result.Height : 0) : 0; }
        }

        /// <summary>
        /// Gets if the image is completely available.
        /// </summary>
        public Boolean IsCompleted
        {
            get { return _current == null || _current.IsCompleted; }
        }

        /// <summary>
        /// Gets or sets if the image element is a map. The attribute must not
        /// be specified on an element that does not have an ancestor a element
        /// with an href attribute.
        /// </summary>
        public Boolean IsMap
        {
            get { return GetOwnAttribute(AttributeNames.IsMap) != null; }
            set { SetOwnAttribute(AttributeNames.IsMap, value ? String.Empty : null); }
        }

        #endregion

        #region Methods

        void UpdateSource(String value)
        {
            Owner.Tasks.Cancel(_current);

            if (!String.IsNullOrEmpty(value))
            {
                var url = new Url(ActualSource);
                var request = this.CreateRequestFor(url);
                //TODO Implement with srcset etc. --> see:
                //http://www.w3.org/html/wg/drafts/html/master/embedded-content.html#update-the-image-data
                _current = Owner.LoadResource<IImageInfo>(request);
                _current.ContinueWith(m =>
                {
                    if (m.IsFaulted || m.Exception != null)
                    {
                        this.FireSimpleEvent(EventNames.Error);
                        return;
                    }

                    this.FireSimpleEvent(EventNames.Load);
                });
            }
        }

        #endregion
    }
}
