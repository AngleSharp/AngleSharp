namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The plaintext HTML element.
    /// </summary>
    sealed class HtmlPlaintextElement : HtmlElement
    {
        public HtmlPlaintextElement(Document owner, String prefix)
            : base(owner, TagNames.Plaintext, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
