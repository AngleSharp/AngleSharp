namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services.Media;
    using System;

    /// <summary>
    /// Represents the embed element.
    /// </summary>
    sealed class HtmlEmbedElement : HtmlElement, IHtmlEmbedElement
    {
        #region Fields

        IObjectInfo _obj;
        IDownload _download;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new Embed element.
        /// </summary>
        public HtmlEmbedElement(Document owner, String prefix = null)
            : base(owner, Tags.Embed, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

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
            RegisterAttributeObserver(AttributeNames.Src, UpdateSource);

            if (src != null)
            {
                UpdateSource(src);
            }
        }

        #endregion

        #region Helpers

        void UpdateSource(String value)
        {
            if (_download != null)
            {
                _download.Cancel();
            }

            var document = Owner;

            if (!String.IsNullOrEmpty(value) && document != null)
            {
                var loader = document.Loader;

                if (loader != null)
                {
                    var url = new Url(Source);
                    var request = this.CreateRequestFor(url);
                    var download = loader.DownloadAsync(request);
                    var task = this.ProcessResource<IObjectInfo>(download, result => _obj = result);
                    document.DelayLoad(task);
                    _download = download;

                }
            }
        }

        #endregion
    }
}
