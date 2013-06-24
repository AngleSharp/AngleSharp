using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the select element.
    /// </summary>
    public sealed class HTMLSelectElement : HTMLFormControlElementWithState
    {
        #region Constant

        /// <summary>
        /// The select tag.
        /// </summary>
        internal const string Tag = "select";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML select element.
        /// </summary>
        internal HTMLSelectElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        public HTMLFormElement Form
        {
            get { return GetAssignedForm(); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public String Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Gets or sets if the select element is enabled or disabled.
        /// </summary>
        public Boolean Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the select element field is required.
        /// </summary>
        public Boolean Required
        {
            get { return GetAttribute("required") != null; }
            set { SetAttribute("required", value ? string.Empty : null); }
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
    }
}
