using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the keygen element.
    /// </summary>
    public sealed class HTMLKeygenElement : HTMLFormControlElementWithState, IValidation
    {
        #region Constant

        /// <summary>
        /// The keygen tag.
        /// </summary>
        internal const string Tag = "keygen";

        #endregion

        #region Members

        ValidityState vstate;
        private string error;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML keygen element.
        /// </summary>
        internal HTMLKeygenElement()
        {
            vstate = new ValidityState();
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
        /// Gets or sets the challenge attribute.
        /// </summary>
        public string Challenge
        {
            get { return GetAttribute("challenge"); }
            set { SetAttribute("challenge", value); }
        }

        /// <summary>
        /// Gets or sets the type of key used.
        /// </summary>
        public Encryption Keytype
        {
            get { return ToEnum(GetAttribute("keytype"), Encryption.RSA); }
            set { SetAttribute("keytype", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets if the keygen control should have focus when the page loads.
        /// </summary>
        public bool Autofocus
        {
            get { return GetAttribute("autofocus") != null; }
            set { SetAttribute("autofocus", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets the type of input control (keygen).
        /// </summary>
        public string Type
        {
            get { return Tag; }
        }

        /// <summary>
        /// Gets the current validation message.
        /// </summary>
        public string ValidationMessage
        {
            get { return vstate.CustomError ? error : string.Empty; }
        }

        /// <summary>
        /// Gets the boolean value false since keygen elements do not validate.
        /// </summary>
        public bool WillValidate
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the current validation state of the keygen element.
        /// </summary>
        public ValidityState Validity
        {
            get { return vstate; }
        }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        public HTMLFormElement Form
        {
            get { return GetAssignedForm(); }
        }

        /// <summary>
        /// Gets or sets if the element is enabled or disabled.
        /// </summary>
        public bool Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? string.Empty : null); }
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

        #region Methods

        /// <summary>
        /// Checks the validity. This is always true since keygen elements are
        /// not candidites for constraint validation.
        /// </summary>
        /// <returns>True.</returns>
        public bool CheckValidity()
        {
            return true;
        }

        /// <summary>
        /// Sets a custom validation error. If this is not the empty string,
        /// then the element is suffering from a custom validation error.
        /// </summary>
        /// <param name="error"></param>
        public void SetCustomValidity(string error)
        {
            vstate.CustomError = !string.IsNullOrEmpty(error);
            this.error = error;
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// An enumeration of possible keytype values.
        /// </summary>
        public enum Encryption : ushort
        {
            /// <summary>
            /// The RSA encryption.
            /// </summary>
            RSA
        }

        #endregion
    }
}
