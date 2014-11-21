namespace AngleSharp.DOM.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.DOM.Collections;
    using AngleSharp.Html;
    using System;
    using System.Threading;

    /// <summary>
    /// Represents the HTML iframe element.
    /// </summary>
    sealed class HTMLIFrameElement : HTMLFrameElementBase, IHtmlInlineFrameElement
    {
        #region Fields

        SettableTokenList _sandbox;
        Document _doc;
        
        #endregion

        #region ctor

        internal HTMLIFrameElement()
            : base(Tags.Iframe, NodeFlags.LiteralText)
        {
            _doc = new Document();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        public Alignment Align
        {
            get { return GetAttribute(AttributeNames.Align).ToEnum(Alignment.Bottom); }
            set { SetAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets the content of the page that the nested browsing context is to contain.
        /// </summary>
        public String ContentHtml
        {
            get { return GetAttribute(AttributeNames.SrcDoc); }
            set { SetAttribute(AttributeNames.SrcDoc, value); }
        }

        public ISettableTokenList Sandbox
        {
            get
            { 
                if (_sandbox == null)
                {
                    _sandbox = new SettableTokenList(GetAttribute(AttributeNames.Sandbox));
                    _sandbox.Changed += (s, ev) => UpdateAttribute(AttributeNames.Sandbox, _sandbox.Value);
                }

                return _sandbox;
            }
        }

        /// <summary>
        /// Gets or sets the value of the seamless attribute.
        /// </summary>
        public Boolean IsSeamless
        {
            get { return GetAttribute(AttributeNames.SrcDoc) != null; }
            set { SetAttribute(AttributeNames.SrcDoc, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the frame's content can trigger the fullscreen mode.
        /// </summary>
        public Boolean IsFullscreenAllowed
        {
            get { return GetAttribute(AttributeNames.AllowFullscreen) != null; }
            set { SetAttribute(AttributeNames.AllowFullscreen, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the frame's parent's window context.
        /// </summary>
        public IWindow ContentWindow
        {
            get { return _doc != null ? _doc.DefaultView : null; }
        }

        #endregion

        #region Methods

        internal override void Close()
        {
            base.Close();
            var src = Source;

            if (src == null)
                return;
            
            var url = this.HyperRef(src);
            var requester = Owner.Options.GetRequester(url.Scheme);

            if (requester == null)
                return;

            requester.LoadAsync(url).ContinueWith(task =>
            {
                if (!task.IsFaulted && task.Result != null)
                {
                    using (var result = task.Result)
                        _doc.LoadAsync(result, CancellationToken.None).Wait();
                }
            });
        }

        #endregion
    }
}
