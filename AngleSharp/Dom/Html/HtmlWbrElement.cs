namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML wbr (word-break-opportunity) element.
    /// This element is used to indicate that the position is a good
    /// point for inserting a possible line-break.
    /// </summary>
    sealed class HtmlWbrElement : HtmlElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML wbr element.
        /// </summary>
        public HtmlWbrElement(Document owner, String prefix = null)
            : base(owner, Tags.Wbr, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion
    }
}
