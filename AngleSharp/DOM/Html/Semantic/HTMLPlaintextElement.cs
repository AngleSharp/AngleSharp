namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The plaintext HTML element.
    /// </summary>
    sealed class HTMLPlaintextElement : HTMLElement
    {
        internal HTMLPlaintextElement()
            : base(Tags.Plaintext, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
