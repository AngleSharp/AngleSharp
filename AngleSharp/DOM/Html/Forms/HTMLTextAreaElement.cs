using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a HTML textarea element.
    /// </summary>
    [DOM("HTMLTextAreaElement")]
    public sealed class HTMLTextAreaElement : HTMLTextFormControlElement
    {
        #region Constant

        /// <summary>
        /// The textarea tag.
        /// </summary>
        internal const String Tag = "textarea";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML textarea element.
        /// </summary>
        internal HTMLTextAreaElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

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
        /// Gets or sets if the textarea is enabled or disabled.
        /// </summary>
        [DOM("disabled")]
        public Boolean Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the textarea field is required.
        /// </summary>
        [DOM("required")]
        public Boolean Required
        {
            get { return GetAttribute("required") != null; }
            set { SetAttribute("required", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the textarea field is read-only.
        /// </summary>
        [DOM("readonly")]
        public Boolean Readonly
        {
            get { return GetAttribute("readonly") != null; }
            set { SetAttribute("readonly", value ? String.Empty : null); }
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

        internal Boolean IsMutable
        {
            get { return !Disabled && !Readonly; }
        }

        #endregion
    }
}
