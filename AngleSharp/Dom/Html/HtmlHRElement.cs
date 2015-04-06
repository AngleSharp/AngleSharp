namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the hr element.
    /// </summary>
    sealed class HtmlHrElement : HtmlElement, IHtmlHrElement
    {
        #region ctor

        /// <summary>
        /// Creates a new hr element.
        /// </summary>
        public HtmlHrElement(Document owner, String prefix = null)
            : base(owner, Tags.Hr, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion
    }
}
