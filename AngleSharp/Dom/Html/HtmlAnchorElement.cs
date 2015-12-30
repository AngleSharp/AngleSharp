namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an anchor element.
    /// </summary>
    sealed class HtmlAnchorElement : HtmlUrlBaseElement, IHtmlAnchorElement
    {
        #region ctor

        /// <summary>
        /// Creates a new anchor element.
        /// </summary>
        public HtmlAnchorElement(Document owner, String prefix = null)
            : base(owner, TagNames.A, prefix, NodeFlags.HtmlFormatting)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the character encoding for the target resource.
        /// </summary>
        public String Charset
        {
            get { return this.GetOwnAttribute(AttributeNames.Charset); }
            set { this.SetOwnAttribute(AttributeNames.Charset, value); }
        }

        /// <summary>
        /// Gets or sets the anchor name.
        /// </summary>
        public String Name
        {
            get { return this.GetOwnAttribute(AttributeNames.Name); }
            set { this.SetOwnAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets or sets the text of the anchor tag (same as TextContent).
        /// </summary>
        public String Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        #endregion

        #region Helpers

        public override void DoFocus()
        {
            if (this.HasOwnAttribute(AttributeNames.Href))
                IsFocused = true;
        }

        #endregion
    }
}
