namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The xmp HTML element.
    /// </summary>
    sealed class HTMLXmpElement : HTMLElement
    {
        internal HTMLXmpElement()
            : base(Tags.Xmp, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
