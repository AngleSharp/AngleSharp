namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML pre element.
    /// </summary>
    sealed class HtmlPreElement : HtmlElement, IHtmlPreElement
    {
        #region ctor

        public HtmlPreElement(Document owner)
            : base(owner, Tags.Pre, NodeFlags.Special | NodeFlags.LineTolerance)
        {
        }

        #endregion
    }
}
