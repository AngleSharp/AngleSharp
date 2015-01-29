namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The plaintext HTML element.
    /// </summary>
    sealed class HTMLPlaintextElement : HtmlElement
    {
        public HTMLPlaintextElement(Document owner)
            : base(owner, Tags.Plaintext, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
