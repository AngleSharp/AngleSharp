namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the HTML menu element.
    /// </summary>
    sealed class HtmlMenuElement : HtmlElement, IHtmlMenuElement
    {
        #region Fields

        private const String ContextType = "context";
        private const String ToolbarType = "toolbar";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML menu element.
        /// </summary>
        public HtmlMenuElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Menu, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of the menu element: context or toolbar (the
        /// default), unless it is in a context menu, whose type it has then.
        /// </summary>
        public String? Type
        {
            get
            {
                var type = this.GetOwnAttribute(AttributeNames.Type);

                if (type.Isi(ContextType))
                {
                    return ContextType;
                }
                else if (type.Isi(ToolbarType))
                {
                    return ToolbarType;
                }

                return Parent is HtmlMenuElement menu && menu.Type == ContextType ? ContextType : ToolbarType;
            }
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        /// <summary>
        /// Gets or sets the text label of the menu element.
        /// </summary>
        public String? Label
        {
            get => this.GetOwnAttribute(AttributeNames.Label);
            set => this.SetOwnAttribute(AttributeNames.Label, value);
        }

        #endregion
    }
}
