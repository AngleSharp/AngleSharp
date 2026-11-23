namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Io.Processors;
    using AngleSharp.Text;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the image element.
    /// </summary>
    sealed class HtmlImageElement : HtmlElement, IHtmlImageElement
    {
        #region Fields

        private ImageRequestProcessor? _request;

        #endregion

        #region ctor
        
        public HtmlImageElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Img, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
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

        /// <summary>
        /// Loads the given source as the image (see <see cref="ImageExtensions.LoadAsync(IHtmlImageElement, Url, Double)"/>):
        /// the element itself requests nothing, as only what shows it knows which candidate
        /// fits and when it is needed.
        /// </summary>
        internal Task LoadAsync(Url source, Double density)
        {
            // created on the first load, as most images are only ever parsed
            var request = _request ??= CreateRequest(Owner);

            if (request is null)
            {
                return Task.CompletedTask;
            }

            request.Density = density > 0 && !Double.IsInfinity(density) ? density : 1.0;

            var task = request.ProcessAsync(this.CreateRequestFor(source));
            Owner.DelayLoad(task);
            return task;
        }

        /// <summary>
        /// An adopted image loads with the loader of its new document, once it is asked to.
        /// </summary>
        protected override void NodeIsAdopted(Document oldDocument)
        {
            base.NodeIsAdopted(oldDocument);

            if (!ReferenceEquals(oldDocument.Context, Owner.Context))
            {
                _request = null;
            }
        }

        // The document has asked its context for the loader once already. Without one there is
        // nothing a processor could do.
        private static ImageRequestProcessor? CreateRequest(Document owner) =>
            owner.ResourceLoader is not null ? new ImageRequestProcessor(owner.Context) : null;

        #endregion
    }
}
