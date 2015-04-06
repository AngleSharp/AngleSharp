namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML head element.
    /// </summary>
    sealed class HtmlHeadElement : HtmlElement, IHtmlHeadElement
    {
        #region ctor

        public HtmlHeadElement(Document owner, String prefix = null)
            : base(owner, Tags.Head, prefix, NodeFlags.Special)
        {
        }

        #endregion
    }
}