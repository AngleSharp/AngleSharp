namespace AngleSharp.Dom.Html
{
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
        public HtmlHrElement(Document owner)
            : base(owner, Tags.Hr, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion
    }
}
