namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML pre element.
    /// </summary>
    sealed class HtmlPreElement : HtmlElement, IHtmlPreElement
    {
        #region ctor

        public HtmlPreElement(Document owner, String prefix = null)
            : base(owner, Tags.Pre, prefix, NodeFlags.Special | NodeFlags.LineTolerance)
        {
        }

        #endregion
    }
}
