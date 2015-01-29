namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML dl element.
    /// </summary>
    sealed class HtmlDefinitionListElement : HtmlElement
    {
        #region ctor

        public HtmlDefinitionListElement(Document owner)
            : base(owner, Tags.Dl, NodeFlags.Special)
        {
        }

        #endregion
    }
}
