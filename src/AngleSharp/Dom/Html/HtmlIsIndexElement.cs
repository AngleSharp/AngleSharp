namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML isindex element.
    /// </summary>
    sealed class HtmlIsIndexElement : HtmlElement
    {
        #region ctor

        public HtmlIsIndexElement(Document owner, String prefix = null)
            : base(owner, TagNames.IsIndex, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        public IHtmlFormElement Form
        {
            get;
            internal set;
        }

        public String Prompt
        {
            get { return this.GetOwnAttribute(AttributeNames.Prompt); }
            set { this.SetOwnAttribute(AttributeNames.Prompt, value); }
        }

        #endregion
    }
}
