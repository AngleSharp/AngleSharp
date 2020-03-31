namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the HTML frame element.
    /// </summary>
    sealed class HtmlFrameElement : HtmlFrameElementBase
    {
        #region ctor

        public HtmlFrameElement(Document owner, String prefix = null)
            : base(owner, TagNames.Frame, prefix, NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        public Boolean NoResize
        {
            get => this.GetOwnAttribute(AttributeNames.NoResize).ToBoolean(false);
            set => this.SetOwnAttribute(AttributeNames.NoResize, value.ToString());
        }

        #endregion
    }
}
