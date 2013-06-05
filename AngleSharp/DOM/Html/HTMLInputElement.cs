using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML input element.
    /// </summary>
    public sealed class HTMLInputElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The input tag.
        /// </summary>
        public const string Tag = "input";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML input element.
        /// </summary>
        public HTMLInputElement()
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
        /// Gets the associated HTML form element.
        /// </summary>
        public HTMLFormElement Form
        {
            get { return GetAssignedForm(); }
        }

        /// <summary>
        /// Gets or sets if the input element is checked or not.
        /// </summary>
        public bool Checked
        {
            get { return GetAttribute("checked") != null; }
            set { SetAttribute("checked", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the input element is enabled or disabled.
        /// </summary>
        public bool Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the type of the input field.
        /// </summary>
        public InputType Type
        {
            get { return ToEnum(GetAttribute("type"), InputType.Text); }
            set { SetAttribute("type", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets if the input field is required.
        /// </summary>
        public bool Required
        {
            get { return GetAttribute("required") != null; }
            set { SetAttribute("required", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the input field is read-only.
        /// </summary>
        public bool Readonly
        {
            get { return GetAttribute("readonly") != null; }
            set { SetAttribute("readonly", value ? string.Empty : null); }
        }

        //TODO
        //http://www.w3.org/html/wg/drafts/html/master/forms.html#htmlinputelement

        public bool Indeterminate { get; set; }

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

        #region Methods

        #endregion

        #region Internal Properties

        internal bool IsMutable 
        {
            get { return !Disabled && !Readonly; }
        }

        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// An enumeration with possible input types.
        /// </summary>
        public enum InputType : ushort
        {
            /// <summary>
            /// The input will be hidden.
            /// </summary>
            Hidden,
            /// <summary>
            /// A standard (1-line) text input.
            /// </summary>
            Text,
            /// <summary>
            /// A search input.
            /// </summary>
            Search,
            /// <summary>
            /// A telephone number input.
            /// </summary>
            Tel,
            /// <summary>
            /// An URL input field.
            /// </summary>
            Url,
            /// <summary>
            /// An email input field.
            /// </summary>
            Email,
            /// <summary>
            /// A password input field.
            /// </summary>
            Password,
            /// <summary>
            /// A datetime input field.
            /// </summary>
            Datetime,
            /// <summary>
            /// A date input field.
            /// </summary>
            Date,
            /// <summary>
            /// A month picker input field.
            /// </summary>
            Month,
            /// <summary>
            /// A week picker input field.
            /// </summary>
            Week,
            /// <summary>
            /// A time picker input field.
            /// </summary>
            Time,
            /// <summary>
            /// A number input field.
            /// </summary>
            Number,
            /// <summary>
            /// A range picker.
            /// </summary>
            Range,
            /// <summary>
            /// A color picker input field.
            /// </summary>
            Color,
            /// <summary>
            /// A checkbox.
            /// </summary>
            Checkbox,
            /// <summary>
            /// A radio box.
            /// </summary>
            Radio,
            /// <summary>
            /// A file upload box.
            /// </summary>
            File,
            /// <summary>
            /// A submit button.
            /// </summary>
            Submit,
            /// <summary>
            /// An image input box.
            /// </summary>
            Image,
            /// <summary>
            /// A reset form button.
            /// </summary>
            Reset,
            /// <summary>
            /// A simple button.
            /// </summary>
            Button
        }

        #endregion
    }
}
