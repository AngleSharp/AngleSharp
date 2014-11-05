namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML dl element.
    /// </summary>
    sealed class HTMLDListElement : HTMLElement
    {
        #region ctor

        internal HTMLDListElement()
            : base(Tags.Dl, NodeFlags.Special)
        {
        }

        #endregion
    }
}
