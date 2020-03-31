namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML menu element.
    /// </summary>
    sealed class HtmlMenuElement : HtmlElement, IHtmlMenuElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML menu element.
        /// </summary>
        public HtmlMenuElement(Document owner, String prefix = null)
            : base(owner, TagNames.Menu, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of the menu element.
        /// </summary>
        public String Type
        {
            get => this.GetOwnAttribute(AttributeNames.Type);
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        /// <summary>
        /// Gets or sets the text label of the menu element.
        /// </summary>
        public String Label
        {
            get => this.GetOwnAttribute(AttributeNames.Label);
            set => this.SetOwnAttribute(AttributeNames.Label, value);
        }

        #endregion
    }
}
