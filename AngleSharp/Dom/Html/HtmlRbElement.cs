namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The rb HTML element.
    /// </summary>
    sealed class HtmlRbElement : HtmlElement
    {
        public HtmlRbElement(Document owner, String prefix = null)
            : base(owner, Tags.Rb, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
