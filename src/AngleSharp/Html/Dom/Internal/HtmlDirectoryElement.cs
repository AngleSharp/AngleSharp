namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML dir element.
    /// This element is obsolete since HTML 4.01.
    /// </summary>
    [DomHistorical]
    sealed class HtmlDirectoryElement : HtmlElement
    {
        public HtmlDirectoryElement(Document owner, String prefix = null)
            : base (owner, TagNames.Dir, prefix, NodeFlags.Special)
        {
        }
    }
}
