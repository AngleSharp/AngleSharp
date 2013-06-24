using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a HTML textarea element.
    /// </summary>
    public sealed class HTMLTextAreaElement : HTMLTextFormControlElement
    {
        #region Constant

        /// <summary>
        /// The textarea tag.
        /// </summary>
        internal const string Tag = "textarea";

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
        public string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Gets or sets if the textarea is enabled or disabled.
        /// </summary>
        public bool Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the textarea field is required.
        /// </summary>
        public bool Required
        {
            get { return GetAttribute("required") != null; }
            set { SetAttribute("required", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the textarea field is read-only.
        /// </summary>
        public bool Readonly
        {
            get { return GetAttribute("readonly") != null; }
            set { SetAttribute("readonly", value ? string.Empty : null); }
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

        internal bool IsMutable
        {
            get { return !Disabled && !Readonly; }
        }

        #endregion
    }
}
