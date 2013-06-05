using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML menuitem element.
    /// </summary>
    public sealed class HTMLMenuItemElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The menuitem tag.
        /// </summary>
        public const string Tag = "menuitem";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML menuitem element.
        /// </summary>
        public HTMLMenuItemElement()
        {
            _name = Tag;
        }

        #endregion

        #region Design properties

        /// <summary>
        /// Gets or sets if the link has been visited.
        /// </summary>
        internal bool IsVisited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the link is currently active.
        /// </summary>
        internal bool IsActive
        {
            get;
            set;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assigned master command, if any.
        /// </summary>
        public Element Command
        {
            get
            {
                var id = GetAttribute("command");

                if (!string.IsNullOrEmpty(id) && _owner != null)
                    return _owner.GetElementById(id);

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the type of command.
        /// </summary>
        public ItemType Type
        {
            get { return ToEnum(GetAttribute("type"), ItemType.Command); }
            set { SetAttribute("type", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the user-visible label.
        /// </summary>
        public string Label
        {
            get { return GetAttribute("label"); }
            set { SetAttribute("label", value); }
        }

        /// <summary>
        /// Gets or sets the icon for the command.
        /// </summary>
        public string Icon
        {
            get { return GetAttribute("icon"); }
            set { SetAttribute("icon", value); }
        }

        /// <summary>
        /// Gets or sets if the menuitem element is enabled or disabled.
        /// </summary>
        public bool Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the menuitem element is checked or not.
        /// </summary>
        public bool Checked
        {
            get { return GetAttribute("checked") != null; }
            set { SetAttribute("checked", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the menuitem element is the default command.
        /// </summary>
        public bool Default
        {
            get { return GetAttribute("default") != null; }
            set { SetAttribute("default", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the name of group of commands to
        /// treat as a radio button group.
        /// </summary>
        public string Radiogroup
        {
            get { return GetAttribute("radiogroup"); }
            set { SetAttribute("radiogroup", value); }
        }

        #endregion

        #region Internal properties

        protected internal override bool IsSpecial
        {
            get { return true; }
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
