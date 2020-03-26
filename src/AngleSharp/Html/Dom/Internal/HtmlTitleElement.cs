namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the title element.
    /// </summary>
    sealed class HtmlTitleElement : HtmlElement, IHtmlTitleElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML title element.
        /// </summary>
        public HtmlTitleElement(Document owner, String prefix = null)
            : base(owner, TagNames.Title, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the title.
        /// </summary>
        public String Text
        {
            get => TextContent;
            set => TextContent = value;
        }

        #endregion
    }
}
