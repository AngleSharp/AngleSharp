namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML head element.
    /// </summary>
    sealed class HTMLHeadElement : HTMLElement, IHtmlHeadElement
    {
        #region ctor

        public HTMLHeadElement(Document owner)
            : base(owner, Tags.Head, NodeFlags.Special)
        {
        }

        #endregion
    }
}