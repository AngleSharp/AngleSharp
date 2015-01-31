namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML picture element.
    /// </summary>
    sealed class HtmlPictureElement : HtmlElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML picture element.
        /// </summary>
        public HtmlPictureElement(Document owner)
            : base(owner, Tags.Picture)
        {
        }

        #endregion
    }
}
