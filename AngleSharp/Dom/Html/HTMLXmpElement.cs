namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The xmp HTML element.
    /// </summary>
    sealed class HtmlXmpElement : HtmlElement
    {
        public HtmlXmpElement(Document owner)
            : base(owner, Tags.Xmp, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
