namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML div element.
    /// </summary>
    sealed class HTMLDivElement : HTMLElement, IHtmlDivElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML div element.
        /// </summary>
        internal HTMLDivElement()
            : base(Tags.Div, NodeFlags.Special)
        {
        }

        #endregion
    }
}
