namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Io.Processors;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the HTML object element.
    /// </summary>
    sealed class HtmlObjectElement : HtmlFormControlElement, IHtmlObjectElement
    {
        #region Fields

        private readonly ObjectRequestProcessor _request;

        #endregion

        #region ctor
        
        public HtmlObjectElement(Document owner, String prefix = null)
            : base(owner, TagNames.Object, prefix, NodeFlags.Scoped)
        {
            _request = new ObjectRequestProcessor(owner.Context);
        }

        #endregion

        #region Properties

        public IDownload CurrentDownload => _request?.Download;

        public String Source
        {
            get => this.GetUrlAttribute(AttributeNames.Data);
            set => this.SetOwnAttribute(AttributeNames.Data, value);
        }

        public String Type
        {
            get => this.GetOwnAttribute(AttributeNames.Type);
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        public Boolean TypeMustMatch
        {
            get => this.GetBoolAttribute(AttributeNames.TypeMustMatch);
            set => this.SetBoolAttribute(AttributeNames.TypeMustMatch, value);
        }

        public String UseMap
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

        public IDocument ContentDocument => null;

        public IWindow ContentWindow => null;

        #endregion

        #region Methods

        protected override Boolean CanBeValidated()
        {
            return false;
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();

            var data = this.GetOwnAttribute(AttributeNames.Data);

            if (data != null)
            {
                UpdateSource(data);
            }
        }

        internal void UpdateSource(String value)
        {
            var url = new Url(Source);
            this.Process(_request, url);
        }

        #endregion
    }
}
