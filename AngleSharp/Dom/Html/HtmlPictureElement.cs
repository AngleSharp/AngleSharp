namespace AngleSharp.Dom.Html
{
    using System;
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
        public HtmlPictureElement(Document owner, String prefix = null)
            : base(owner, Tags.Picture, prefix)
        {
        }

        #endregion
    }
}
