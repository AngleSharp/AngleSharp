namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML span element.
    /// </summary>
    sealed class HtmlSpanElement : HtmlElement, IHtmlSpanElement
    {
        #region ctor

        public HtmlSpanElement(Document owner, String prefix = null)
            : base(owner, Tags.Span, prefix)
        {
            Owner = owner;
        }

        #endregion
    }
}
