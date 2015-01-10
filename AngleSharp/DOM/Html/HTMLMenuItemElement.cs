namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML menuitem element.
    /// </summary>
    sealed class HTMLMenuItemElement : HTMLElement, IHtmlMenuItemElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML menuitem element.
        /// </summary>
        public HTMLMenuItemElement(Document owner)
            : base(Tags.MenuItem, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            Owner = owner;
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
                var id = GetAttribute(AttributeNames.Command);

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
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets the user-visible label.
        /// </summary>
        public String Label
        {
            get { return GetAttribute(AttributeNames.Label); }
            set { SetAttribute(AttributeNames.Label, value); }
        }

        /// <summary>
        /// Gets or sets the icon for the command.
        /// </summary>
        public String Icon
        {
            get { return GetAttribute(AttributeNames.Icon); }
            set { SetAttribute(AttributeNames.Icon, value); }
        }

        /// <summary>
        /// Gets or sets if the menuitem element is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { return GetAttribute(AttributeNames.Disabled) != null; }
            set { SetAttribute(AttributeNames.Disabled, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the menuitem element is checked or not.
        /// </summary>
        public Boolean IsChecked
        {
            get { return GetAttribute(AttributeNames.Checked) != null; }
            set { SetAttribute(AttributeNames.Checked, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the menuitem element is the default command.
        /// </summary>
        public Boolean IsDefault
        {
            get { return GetAttribute(AttributeNames.Default) != null; }
            set { SetAttribute(AttributeNames.Default, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the name of group of commands to
        /// treat as a radio button group.
        /// </summary>
        public String RadioGroup
        {
            get { return GetAttribute(AttributeNames.Radiogroup); }
            set { SetAttribute(AttributeNames.Radiogroup, value); }
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// An enumeration of possible type states for a menuitem.
        /// </summary>
        public enum ItemType : ushort
        {
            /// <summary>
            /// The command state.
            /// </summary>
            Command,
            /// <summary>
            /// The checkbox state.
            /// </summary>
            Checkbox,
            /// <summary>
            /// The radio state.
            /// </summary>
            Radio
        }

        #endregion
    }
}
