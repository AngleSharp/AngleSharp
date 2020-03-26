namespace AngleSharp.Html.Dom
{
    using AngleSharp.Browser;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML meta element.
    /// </summary>
    sealed class HtmlMetaElement : HtmlElement, IHtmlMetaElement
    {
        #region ctor

        public HtmlMetaElement(Document owner, String prefix = null)
            : base(owner, TagNames.Meta, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        public String Content
        {
            get => this.GetOwnAttribute(AttributeNames.Content);
            set => this.SetOwnAttribute(AttributeNames.Content, value);
        }

        public String Charset
        {
            get => this.GetOwnAttribute(AttributeNames.Charset);
            set => this.SetOwnAttribute(AttributeNames.Charset, value);
        }

        public String HttpEquivalent
        {
            get => this.GetOwnAttribute(AttributeNames.HttpEquiv);
            set => this.SetOwnAttribute(AttributeNames.HttpEquiv, value);
        }

        public String Scheme
        {
            get => this.GetOwnAttribute(AttributeNames.Scheme);
            set => this.SetOwnAttribute(AttributeNames.Scheme, value);
        }

        public String Name
        {
            get => this.GetOwnAttribute(AttributeNames.Name);
            set => this.SetOwnAttribute(AttributeNames.Name, value);
        }

        #endregion

        #region Methods

        public void Handle()
        {
            var handlers = Owner.Context.GetServices<IMetaHandler>();

            foreach (var handler in handlers)
            {
                handler.HandleContent(this);
            }
        }

        #endregion
    }
}
