using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML button element.
    /// </summary>
    [DOM("HTMLButtonElement")]
    public sealed class HTMLButtonElement : HTMLFormControlElement
    {
        #region Constant

        /// <summary>
        /// The button tag.
        /// </summary>
        internal const String Tag = "button";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML button element.
        /// </summary>
        internal HTMLButtonElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the input element is enabled or disabled.
        /// </summary>
        [DOM("disabled")]
        public Boolean Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        [DOM("name")]
        public String Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        [DOM("form")]
        public HTMLFormElement Form
        {
            get { return GetAssignedForm(); }
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

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
