namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML dl element.
    /// </summary>
    sealed class HTMLDListElement : HTMLElement
    {
        #region ctor

        public HTMLDListElement(Document owner)
            : base(Tags.Dl, NodeFlags.Special)
        {
            Owner = owner;
        }

        #endregion
    }
}
