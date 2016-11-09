namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML picture element.
    /// </summary>
    sealed class HtmlPictureElement : HtmlElement
    {
        public HtmlPictureElement(Document owner, String prefix = null)
            : base(owner, TagNames.Picture, prefix)
        {
        }
    }
}
