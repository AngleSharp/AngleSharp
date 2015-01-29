namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The plaintext HTML element.
    /// </summary>
    sealed class HtmlPlaintextElement : HtmlElement
    {
        public HtmlPlaintextElement(Document owner)
            : base(owner, Tags.Plaintext, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
