namespace AngleSharp.Dom.Html
{
    using System;

    /// <summary>
    /// The big HTML element.
    /// </summary>
    sealed class HtmlBigElement : HtmlElement
    {
        public HtmlBigElement(Document owner, String prefix = null)
            : base(owner, TagNames.Big, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
