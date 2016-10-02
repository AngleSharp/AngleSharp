namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Network.RequestProcessors;
    using System;

    /// <summary>
    /// Represents the image element.
    /// </summary>
    sealed class HtmlImageElement : HtmlElement, IHtmlImageElement
    {
        #region Fields

        private readonly ImageRequestProcessor _request;

        #endregion

        #region ctor

        static HtmlImageElement()
        {
            RegisterCallback<HtmlImageElement>(AttributeNames.Src, (element, value) => element.UpdateSource());
            RegisterCallback<HtmlImageElement>(AttributeNames.SrcSet, (element, value) => element.UpdateSource());
            RegisterCallback<HtmlImageElement>(AttributeNames.Sizes, (element, value) => element.UpdateSource());
            RegisterCallback<HtmlImageElement>(AttributeNames.CrossOrigin, (element, value) => element.UpdateSource());
        }

        public HtmlImageElement(Document owner, String prefix = null)
            : base(owner, TagNames.Img, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            _request = ImageRequestProcessor.Create(this);
        }

        #endregion

        #region Properties

        public IDownload CurrentDownload
        {
            get { return _request?.Download; }
        }

        public String ActualSource
        {
            get { return IsCompleted ? _request.Source : String.Empty; }
        }

        public String SourceSet
        {
            get { return this.GetOwnAttribute(AttributeNames.SrcSet); }
            set { this.SetOwnAttribute(AttributeNames.SrcSet, value); }
        }

        public String Sizes
        {
            get { return this.GetOwnAttribute(AttributeNames.Sizes); }
            set { this.SetOwnAttribute(AttributeNames.Sizes, value); }
        }

        public String Source
        {
            get { return this.GetUrlAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        public String AlternativeText
        {
            get { return this.GetOwnAttribute(AttributeNames.Alt); }
            set { this.SetOwnAttribute(AttributeNames.Alt, value); }
        }

        public String CrossOrigin
        {
            get { return this.GetOwnAttribute(AttributeNames.CrossOrigin); }
            set { this.SetOwnAttribute(AttributeNames.CrossOrigin, value); }
        }

        public String UseMap
        {
            get { return this.GetOwnAttribute(AttributeNames.UseMap); }
            set { this.SetOwnAttribute(AttributeNames.UseMap, value); }
        }

        public Int32 DisplayWidth
        {
            get { return this.GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth); }
            set { this.SetOwnAttribute(AttributeNames.Width, value.ToString()); }
        }

        public Int32 DisplayHeight
        {
            get { return this.GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight); }
            set { this.SetOwnAttribute(AttributeNames.Height, value.ToString()); }
        }

        public Int32 OriginalWidth
        {
            get { return _request?.Width ?? 0; }
        }

        public Int32 OriginalHeight
        {
            get { return _request?.Height ?? 0; }
        }

        public Boolean IsCompleted
        {
            get { return _request?.IsReady ?? false; }
        }

        public Boolean IsMap
        {
            get { return this.GetBoolAttribute(AttributeNames.IsMap); }
            set { this.SetBoolAttribute(AttributeNames.IsMap, value); }
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();
            UpdateSource();
        }

        #endregion

        #region Helpers

        private void UpdateSource()
        {
            var url = this.GetImageCandidate();
            this.Process(_request, url);
        }

        #endregion
    }
}
