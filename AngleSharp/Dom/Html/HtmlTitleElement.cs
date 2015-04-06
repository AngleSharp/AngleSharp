namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the title element.
    /// </summary>
    sealed class HtmlTitleElement : HtmlElement, IHtmlTitleElement
    {
        /// <summary>
        /// Creates a new HTML title element.
        /// </summary>
        public HtmlTitleElement(Document owner, String prefix = null)
            : base(owner, Tags.Title, prefix, NodeFlags.Special)
        {
        }

        /// <summary>
        /// Gets or sets the text of the title.
        /// </summary>
        public String Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }
    }
}
