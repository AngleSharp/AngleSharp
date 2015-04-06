namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML menuitem element.
    /// </summary>
    sealed class HtmlMenuItemElement : HtmlElement, IHtmlMenuItemElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML menuitem element.
        /// </summary>
        public HtmlMenuItemElement(Document owner, String prefix = null)
            : base(owner, Tags.MenuItem, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Design properties

        /// <summary>
        /// Gets or sets if the link has been visited.
        /// </summary>
        internal Boolean IsVisited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the link is currently active.
        /// </summary>
        internal Boolean IsActive
        {
            get;
            set;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assigned master command, if any.
        /// </summary>
        public IHtmlElement Command
        {
            get
            {
                var id = GetOwnAttribute(AttributeNames.Command);

                if (!String.IsNullOrEmpty(id) && Owner != null)
                    return Owner.GetElementById(id) as IHtmlElement;

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the type of command.
        /// </summary>
        public String Type
        {
            get { return GetOwnAttribute(AttributeNames.Type); }
            set { SetOwnAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets the user-visible label.
        /// </summary>
        public String Label
        {
            get { return GetOwnAttribute(AttributeNames.Label); }
            set { SetOwnAttribute(AttributeNames.Label, value); }
        }

        /// <summary>
        /// Gets or sets the icon for the command.
        /// </summary>
        public String Icon
        {
            get { return GetOwnAttribute(AttributeNames.Icon); }
            set { SetOwnAttribute(AttributeNames.Icon, value); }
        }

        /// <summary>
        /// Gets or sets if the menuitem element is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { return GetOwnAttribute(AttributeNames.Disabled) != null; }
            set { SetOwnAttribute(AttributeNames.Disabled, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the menuitem element is checked or not.
        /// </summary>
        public Boolean IsChecked
        {
            get { return GetOwnAttribute(AttributeNames.Checked) != null; }
            set { SetOwnAttribute(AttributeNames.Checked, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the menuitem element is the default command.
        /// </summary>
        public Boolean IsDefault
        {
            get { return GetOwnAttribute(AttributeNames.Default) != null; }
            set { SetOwnAttribute(AttributeNames.Default, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the name of group of commands to
        /// treat as a radio button group.
        /// </summary>
        public String RadioGroup
        {
            get { return GetOwnAttribute(AttributeNames.Radiogroup); }
            set { SetOwnAttribute(AttributeNames.Radiogroup, value); }
        }

        #endregion
    }
}
