namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML dl element.
    /// </summary>
    sealed class HTMLDListElement : HTMLElement
    {
        #region ctor

        public HTMLDListElement(Document owner)
            : base(owner, Tags.Dl, NodeFlags.Special)
        {
        }

        #endregion
    }
}
