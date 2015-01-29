namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML wbr (word-break-opportunity) element.
    /// This element is used to indicate that the position is a good
    /// point for inserting a possible line-break.
    /// </summary>
    sealed class HTMLWbrElement : HTMLElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML wbr element.
        /// </summary>
        public HTMLWbrElement(Document owner)
            : base(owner, Tags.Wbr, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion
    }
}
