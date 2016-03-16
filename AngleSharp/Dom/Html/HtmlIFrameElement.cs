namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML iframe element.
    /// </summary>
    sealed class HtmlIFrameElement : HtmlFrameElementBase, IHtmlInlineFrameElement
    {
        #region Fields

        SettableTokenList _sandbox;
        
        #endregion

        #region ctor

        public HtmlIFrameElement(Document owner, String prefix = null)
            : base(owner, TagNames.Iframe, prefix, NodeFlags.LiteralText)
        {
        }

        #endregion

        #region Properties

        public Alignment Align
        {
            get { return this.GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Bottom); }
            set { this.SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        public String ContentHtml
        {
            get { return this.GetOwnAttribute(AttributeNames.SrcDoc); }
            set { this.SetOwnAttribute(AttributeNames.SrcDoc, value); }
        }

        public ISettableTokenList Sandbox
        {
            get
            { 
                if (_sandbox == null)
                {
                    _sandbox = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Sandbox));
                    CreateBindings(_sandbox, AttributeNames.Sandbox);
                }

                return _sandbox;
            }
        }

        public Boolean IsSeamless
        {
            get { return this.HasOwnAttribute(AttributeNames.SrcDoc); }
            set { this.SetOwnAttribute(AttributeNames.SrcDoc, value ? String.Empty : null); }
        }

        public Boolean IsFullscreenAllowed
        {
            get { return this.HasOwnAttribute(AttributeNames.AllowFullscreen); }
            set { this.SetOwnAttribute(AttributeNames.AllowFullscreen, value ? String.Empty : null); }
        }

        public IWindow ContentWindow
        {
            get { return NestedContext.Current; }
        }

        #endregion

        #region Internal Methods

        internal override String GetContentHtml()
        {
            return ContentHtml;
        }

        internal override void SetupElement()
        {
            base.SetupElement();

            var srcDoc = this.GetOwnAttribute(AttributeNames.SrcDoc);
            RegisterAttributeObserver(AttributeNames.SrcDoc, UpdateSource);

            if (srcDoc != null)
            {
                UpdateSource(srcDoc);
            }
        }

        #endregion
    }
}
