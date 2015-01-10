namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML pre element.
    /// </summary>
    sealed class HTMLPreElement : HTMLElement, IHtmlPreElement
    {
        #region ctor

        public HTMLPreElement(Document owner)
            : base(owner, Tags.Pre, NodeFlags.Special | NodeFlags.LineTolerance)
        {
        }

        #endregion
    }
}
