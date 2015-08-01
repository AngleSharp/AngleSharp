namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services.Media;
    using System;

    /// <summary>
    /// Represents the image element.
    /// </summary>
    sealed class HtmlImageElement : HtmlElement, IHtmlImageElement
    {
        #region Fields

        Url _lastSource;
        IImageInfo _img;
        SourceSet _srcset;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new image element.
        /// </summary>
        public HtmlImageElement(Document owner, String prefix = null)
            : base(owner, Tags.Img, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            RegisterAttributeObserver(AttributeNames.Src, UpdateSource);
            RegisterAttributeObserver(AttributeNames.SrcSet, UpdateSource);
            RegisterAttributeObserver(AttributeNames.Sizes, UpdateSource);
            RegisterAttributeObserver(AttributeNames.CrossOrigin, UpdateSource);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the actual used image source.
        /// </summary>
        public String ActualSource
        {
            get { return _lastSource != null ? _lastSource.Href : null; }
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
            get { return GetUrlAttribute(AttributeNames.Src); }
            set { SetOwnAttribute(AttributeNames.Src, value); }
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
            get { return IsCompleted ? _img.Width : 0; }
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public Int32 OriginalHeight
        {
            get { return IsCompleted ? _img.Height : 0; }
        }

        /// <summary>
        /// Gets if the image is completely available.
        /// </summary>
        public Boolean IsCompleted
        {
            get { return _img != null; }
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

        /// <summary>
        /// For more information, see:
        /// http://www.w3.org/html/wg/drafts/html/master/embedded-content.html#update-the-image-data
        /// </summary>
        void GetImage(Url source)
        {
            if (source.IsInvalid)
                source = null;
            else if (_lastSource != null && source.Equals(_lastSource))
                return;

            this.CancelTasks();
            _lastSource = source;

            if (source == null)
                return;

            var request = this.CreateRequestFor(source);
            this.LoadResource<IImageInfo>(request).ContinueWith(m =>
            {
                if (m.IsFaulted == false)
                    _img = m.Result;

                this.FireLoadOrErrorEvent(m);
            });
        }

        void UpdateSource(String value)
        {
            if (_srcset == null)
                _srcset = new SourceSet(this);

            foreach (var candidate in _srcset.GetCandidates(SourceSet, Sizes))
            {
                GetImage(candidate.Source);
                return;
            }

            GetImage(Url.Create(Source));
        }

        #endregion
    }
}
