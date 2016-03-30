namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
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
