using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the base class for all HTML text form controls.
    /// </summary>
    public abstract class HTMLTextFormControlElement : HTMLFormControlElementWithState
    {
        internal HTMLTextFormControlElement()
        {
        }

        /// <summary>
        /// Gets or sets the placeholder HTML attribute, containing a hint to
        /// the user about what to enter in the control.
        /// </summary>
        [DOM("placeholder")]
        public String Placeholder
        {
            get { return GetAttribute("placeholder"); }
            set { SetAttribute("placeholder", value); }
        }

        /// <summary>
        /// Gets or sets the accesskey HTML attribute.
        /// </summary>
        [DOM("accessKey")]
        public String AccessKey
        {
            get { return GetAttribute("accesskey"); }
            set { SetAttribute("accesskey", value); }
        }

        /// <summary>
        /// Gets or sets if the field is required.
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
        [DOM("readOnly")]
        public Boolean Readonly
        {
            get { return GetAttribute("readonly") != null; }
            set { SetAttribute("readonly", value ? String.Empty : null); }
        }
    }
}
