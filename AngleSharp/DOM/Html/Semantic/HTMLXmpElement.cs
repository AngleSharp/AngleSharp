namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The xmp HTML element.
    /// </summary>
    sealed class HTMLXmpElement : HTMLElement
    {
        public HTMLXmpElement(Document owner)
            : base(owner, Tags.Xmp, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
