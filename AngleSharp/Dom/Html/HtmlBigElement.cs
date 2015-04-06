namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The big HTML element.
    /// </summary>
    sealed class HtmlBigElement : HtmlElement
    {
        public HtmlBigElement(Document owner, String prefix)
            : base(owner, Tags.Big, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
