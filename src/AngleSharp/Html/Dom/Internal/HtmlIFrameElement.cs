namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the HTML iframe element.
    /// </summary>
    sealed class HtmlIFrameElement : HtmlFrameElementBase, IHtmlInlineFrameElement
    {
        #region Fields

        private SettableTokenList _sandbox;
        
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
            get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Bottom);
            set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
        }

        public String ContentHtml
        {
            get => this.GetOwnAttribute(AttributeNames.SrcDoc);
            set => this.SetOwnAttribute(AttributeNames.SrcDoc, value);
        }

        public ISettableTokenList Sandbox
        {
            get
            { 
                if (_sandbox == null)
                {
                    _sandbox = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Sandbox));
                    _sandbox.Changed += value => UpdateAttribute(AttributeNames.Sandbox, value);
                }

                return _sandbox;
            }
        }

        public Boolean IsSeamless
        {
            get => this.GetBoolAttribute(AttributeNames.SrcDoc);
            set => this.SetBoolAttribute(AttributeNames.SrcDoc, value);
        }

        public Boolean IsFullscreenAllowed
        {
            get => this.GetBoolAttribute(AttributeNames.AllowFullscreen);
            set => this.SetBoolAttribute(AttributeNames.AllowFullscreen, value);
        }

        public Boolean IsPaymentRequestAllowed
        {
            get => this.GetBoolAttribute(AttributeNames.AllowPaymentRequest);
            set => this.SetBoolAttribute(AttributeNames.AllowPaymentRequest, value);
        }

        public String ReferrerPolicy
        {
            get => this.GetOwnAttribute(AttributeNames.ReferrerPolicy);
            set => this.SetOwnAttribute(AttributeNames.ReferrerPolicy, value);
        }

        public IWindow ContentWindow => NestedContext.Current;

        #endregion

        #region Internal Methods

        internal override String GetContentHtml()
        {
            return ContentHtml;
        }

        internal override void SetupElement()
        {
            base.SetupElement();
            
            if (this.GetOwnAttribute(AttributeNames.SrcDoc) != null)
            {
                UpdateSource();
            }
        }

        internal void UpdateSandbox(String value)
        {
            _sandbox?.Update(value);
        }

        #endregion
    }
}
