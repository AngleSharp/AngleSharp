namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Io.Processors;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the image element.
    /// </summary>
    sealed class HtmlImageElement : HtmlElement, IHtmlImageElement
    {
        #region Fields

        private ImageRequestProcessor? _request;
        private Boolean _isSettingUp;

        #endregion

        #region ctor
        
        public HtmlImageElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Img, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            _request = CreateRequest(owner);
        }

        #endregion

        #region Properties

        public IDownload? CurrentDownload => _request?.Download;

        public String ActualSource => _request is { IsReady: true } request ? request.Source : String.Empty;

        public String? SourceSet
        {
            get => this.GetOwnAttribute(AttributeNames.SrcSet);
            set => this.SetOwnAttribute(AttributeNames.SrcSet, value);
        }

        public String? Sizes
        {
            get => this.GetOwnAttribute(AttributeNames.Sizes);
            set => this.SetOwnAttribute(AttributeNames.Sizes, value);
        }

        public String? Source
        {
            get => this.GetUrlAttribute(AttributeNames.Src);
            set => this.SetOwnAttribute(AttributeNames.Src, value);
        }

        public String? AlternativeText
        {
            get => this.GetOwnAttribute(AttributeNames.Alt);
            set => this.SetOwnAttribute(AttributeNames.Alt, value);
        }

        public String? CrossOrigin
        {
            get => this.GetOwnAttribute(AttributeNames.CrossOrigin);
            set => this.SetOwnAttribute(AttributeNames.CrossOrigin, value);
        }

        public String? UseMap
        {
            get => this.GetOwnAttribute(AttributeNames.UseMap);
            set => this.SetOwnAttribute(AttributeNames.UseMap, value);
        }

        public Int32 DisplayWidth
        {
            get => this.GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth);
            set => this.SetOwnAttribute(AttributeNames.Width, value.ToString());
        }

        public Int32 DisplayHeight
        {
            get => this.GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight);
            set => this.SetOwnAttribute(AttributeNames.Height, value.ToString());
        }

        public Int32 OriginalWidth => _request?.Width ?? 0;

        public Int32 OriginalHeight => _request?.Height ?? 0;

        public Boolean IsCompleted => _request?.IsReady ?? false;

        public Boolean IsMap
        {
            get => this.GetBoolAttribute(AttributeNames.IsMap);
            set => this.SetBoolAttribute(AttributeNames.IsMap, value);
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            _isSettingUp = true;
            try
            {
                base.SetupElement();
            }
            finally
            {
                _isSettingUp = false;
            }

            UpdateSourceCore();
        }

        internal void UpdateSource()
        {
            if (_isSettingUp)
            {
                return;
            }

            UpdateSourceCore();
        }

        /// <summary>
        /// Adopting an image is a relevant mutation: its source is resolved against the
        /// new document (e.g. after innerHTML parsed it in a document of its own).
        /// </summary>
        protected override void NodeIsAdopted(Document oldDocument)
        {
            base.NodeIsAdopted(oldDocument);

            // the image loads with the loader of its new document
            if (!ReferenceEquals(oldDocument.Context, Owner.Context))
            {
                _request = CreateRequest(Owner);
            }

            UpdateSource();
        }

        private void UpdateSourceCore()
        {
            if (Owner is { IsFragmentParsing: true })
            {
                return;
            }

            var request = _request;

            // Without a resource loader nothing is fetched whatever the candidate turns out to
            // be, and selecting one means parsing srcset, sizes and a URL - for every image
            // of a document that was only ever going to be parsed.
            if (request is null)
            {
                return;
            }

            var url = this.GetImageCandidate();

            if (url != null)
            {
                this.Process(request, url);
            }
        }

        // The document has asked its context for the loader once already. Without one there is
        // nothing a processor could do, while asking again walks the whole list of services -
        // for every image.
        private static ImageRequestProcessor? CreateRequest(Document owner) =>
            owner.ResourceLoader is not null ? new ImageRequestProcessor(owner.Context) : null;

        #endregion
    }
}
