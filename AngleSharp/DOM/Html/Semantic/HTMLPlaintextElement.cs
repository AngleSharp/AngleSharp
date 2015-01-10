namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The plaintext HTML element.
    /// </summary>
    sealed class HTMLPlaintextElement : HTMLElement
    {
        public HTMLPlaintextElement(Document owner)
            : base(owner, Tags.Plaintext, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
