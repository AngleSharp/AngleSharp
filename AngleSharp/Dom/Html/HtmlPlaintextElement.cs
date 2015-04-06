namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The plaintext HTML element.
    /// </summary>
    sealed class HtmlPlaintextElement : HtmlElement
    {
        public HtmlPlaintextElement(Document owner, String prefix)
            : base(owner, Tags.Plaintext, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
