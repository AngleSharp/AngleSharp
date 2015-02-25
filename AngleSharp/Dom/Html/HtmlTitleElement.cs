namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the title element.
    /// </summary>
    sealed class HtmlTitleElement : HtmlElement, IHtmlTitleElement
    {
        /// <summary>
        /// Creates a new HTML title element.
        /// </summary>
        public HtmlTitleElement(Document owner)
            : base(owner, Tags.Title, NodeFlags.Special)
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
