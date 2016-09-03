namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Network.RequestProcessors;
    using System;

    /// <summary>
    /// Represents the embed element.
    /// </summary>
    sealed class HtmlEmbedElement : HtmlElement, IHtmlEmbedElement
    {
        #region Fields

        private readonly ObjectRequestProcessor _request;

        #endregion

        #region ctor

        static HtmlEmbedElement()
        {
            RegisterCallback<HtmlEmbedElement>(AttributeNames.Src, (element, value) => element.UpdateSource(value));
        }

        public HtmlEmbedElement(Document owner, String prefix = null)
            : base(owner, TagNames.Embed, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
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
            get { return this.GetOwnAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        public String Type
        {
            get { return this.GetOwnAttribute(AttributeNames.Type); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        public String DisplayWidth
        {
            get { return this.GetOwnAttribute(AttributeNames.Width); }
            set { this.SetOwnAttribute(AttributeNames.Width, value); }
        }

        public String DisplayHeight
        {
            get { return this.GetOwnAttribute(AttributeNames.Height); }
            set { this.SetOwnAttribute(AttributeNames.Height, value); }
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();

            var src = this.GetOwnAttribute(AttributeNames.Src);

            if (src != null)
            {
                UpdateSource(src);
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
