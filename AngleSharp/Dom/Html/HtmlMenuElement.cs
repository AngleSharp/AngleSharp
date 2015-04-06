namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
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
            : base(owner, Tags.Menu, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of the menu element.
        /// </summary>
        public String Type
        {
            get { return GetOwnAttribute(AttributeNames.Type); }
            set { SetOwnAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets the text label of the menu element.
        /// </summary>
        public String Label
        {
            get { return GetOwnAttribute(AttributeNames.Label); }
            set { SetOwnAttribute(AttributeNames.Label, value); }
        }

        #endregion
    }
}
