namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the embed element.
    /// </summary>
    sealed class HtmlEmbedElement : HtmlElement, IHtmlEmbedElement
    {
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
            get { return GetOwnAttribute(AttributeNames.Src); }
            set { SetOwnAttribute(AttributeNames.Src, value); }
        }

        public String Type
        {
            get { return GetOwnAttribute(AttributeNames.Type); }
            set { SetOwnAttribute(AttributeNames.Type, value); }
        }

        public String DisplayWidth
        {
            get { return GetOwnAttribute(AttributeNames.Width); }
            set { SetOwnAttribute(AttributeNames.Width, value); }
        }

        public String DisplayHeight
        {
            get { return GetOwnAttribute(AttributeNames.Height); }
            set { SetOwnAttribute(AttributeNames.Height, value); }
        }

        #endregion
    }
}
