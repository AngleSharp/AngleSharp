namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML head element.
    /// </summary>
    sealed class HTMLHeadElement : HTMLElement, IHtmlHeadElement
    {
        #region ctor

        public HTMLHeadElement()
            : base(Tags.Head, NodeFlags.Special)
        {
        }

        #endregion
    }
}