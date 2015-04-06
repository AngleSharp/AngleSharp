namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML div element.
    /// </summary>
    sealed class HtmlDivElement : HtmlElement, IHtmlDivElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML div element.
        /// </summary>
        public HtmlDivElement(Document owner, String prefix = null)
            : base(owner, Tags.Div, prefix, NodeFlags.Special)
        {
        }

        #endregion
    }
}
