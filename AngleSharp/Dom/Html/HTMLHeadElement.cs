namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML head element.
    /// </summary>
    sealed class HtmlHeadElement : HtmlElement, IHtmlHeadElement
    {
        #region ctor

        public HtmlHeadElement(Document owner)
            : base(owner, Tags.Head, NodeFlags.Special)
        {
        }

        #endregion
    }
}