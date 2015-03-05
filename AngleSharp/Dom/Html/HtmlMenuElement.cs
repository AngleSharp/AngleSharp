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
        public HtmlMenuElement(Document owner)
            : base(owner, Tags.Menu, NodeFlags.Special)
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

        #region Enumeration

        /// <summary>
        /// All possible values for the type of the menu.
        /// </summary>
        public enum MenuType : ushort
        {
            /// <summary>
            /// As a context menu.
            /// </summary>
            Popup,
            /// <summary>
            /// Represented as a toolbar.
            /// </summary>
            Toolbar
        }

        #endregion
    }
}
