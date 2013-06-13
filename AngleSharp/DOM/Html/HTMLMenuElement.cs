using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML menu element.
    /// </summary>
    public sealed class HTMLMenuElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The menu tag.
        /// </summary>
        internal const string Tag = "menu";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML menu element.
        /// </summary>
        internal HTMLMenuElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of the menu element.
        /// </summary>
        public MenuType Type
        {
            get { return ToEnum(GetAttribute("type"), MenuType.Popup); }
            set { SetAttribute("type", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the text label of the menu element.
        /// </summary>
        public string Label
        {
            get { return GetAttribute("label"); }
            set { SetAttribute("label", value); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
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
