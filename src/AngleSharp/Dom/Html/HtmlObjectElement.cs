namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Network.RequestProcessors;
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

        static HtmlObjectElement()
        {
            RegisterCallback<HtmlObjectElement>(AttributeNames.Data, (element, value) => element.UpdateSource(value));
        }

        public HtmlObjectElement(Document owner, String prefix = null)
            : base(owner, TagNames.Object, prefix, NodeFlags.Scoped)
        {
            _request = ObjectRequestProcessor.Create(this);
        }

        #endregion

        #region Properties

        public IDownload CurrentDownload
        {
            get { return _request?.Download; }
        }

        public String Source
        {
            get { return this.GetUrlAttribute(AttributeNames.Data); }
            set { this.SetOwnAttribute(AttributeNames.Data, value); }
        }

        public String Type
        {
            get { return this.GetOwnAttribute(AttributeNames.Type); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        public Boolean TypeMustMatch
        {
            get { return this.GetBoolAttribute(AttributeNames.TypeMustMatch); }
            set { this.SetBoolAttribute(AttributeNames.TypeMustMatch, value); }
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

        public IDocument ContentDocument
        {
            get { return null; }
        }

        public IWindow ContentWindow
        {
            get { return null; }
        }

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

        #endregion

        #region Helpers

        private void UpdateSource(String value)
        {
            var url = new Url(Source);
            this.Process(_request, url);
        }

        #endregion
    }
}
