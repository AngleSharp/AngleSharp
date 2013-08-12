using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the keygen element.
    /// </summary>
    [DOM("HTMLKeygenElement")]
    public sealed class HTMLKeygenElement : HTMLFormControlElementWithState, IValidation
    {
        #region Constant

        /// <summary>
        /// The keygen tag.
        /// </summary>
        internal const String Tag = "keygen";

        #endregion

        #region Members

        ValidityState vstate;
        String error;

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
        [DOM("name")]
        public String Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Gets or sets the challenge attribute.
        /// </summary>
        [DOM("challenge")]
        public String Challenge
        {
            get { return GetAttribute("challenge"); }
            set { SetAttribute("challenge", value); }
        }

        /// <summary>
        /// Gets or sets the type of key used.
        /// </summary>
        [DOM("keytype")]
        public Encryption Keytype
        {
            get { return ToEnum(GetAttribute("keytype"), Encryption.RSA); }
            set { SetAttribute("keytype", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets if the keygen control should have focus when the page loads.
        /// </summary>
        [DOM("autofocus")]
        public Boolean Autofocus
        {
            get { return GetAttribute("autofocus") != null; }
            set { SetAttribute("autofocus", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the type of input control (keygen).
        /// </summary>
        [DOM("type")]
        public String Type
        {
            get { return Tag; }
        }

        /// <summary>
        /// Gets the current validation message.
        /// </summary>
        [DOM("validationMessage")]
        public String ValidationMessage
        {
            get { return vstate.CustomError ? error : String.Empty; }
        }

        /// <summary>
        /// Gets the boolean value false since keygen elements do not validate.
        /// </summary>
        [DOM("willValidate")]
        public Boolean WillValidate
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the current validation state of the keygen element.
        /// </summary>
        [DOM("validity")]
        public ValidityState Validity
        {
            get { return vstate; }
        }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        [DOM("form")]
        public HTMLFormElement Form
        {
            get { return GetAssignedForm(); }
        }

        /// <summary>
        /// Gets or sets if the element is enabled or disabled.
        /// </summary>
        [DOM("disabled")]
        public Boolean Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? String.Empty : null); }
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

        #region Methods

        /// <summary>
        /// Checks the validity. This is always true since keygen elements are
        /// not candidites for constraint validation.
        /// </summary>
        /// <returns>True.</returns>
        [DOM("checkValidity")]
        public Boolean CheckValidity()
        {
            return true;
        }

        /// <summary>
        /// Sets a custom validation error. If this is not the empty string,
        /// then the element is suffering from a custom validation error.
        /// </summary>
        /// <param name="error"></param>
        [DOM("setCustomValidity")]
        public void SetCustomValidity(String error)
        {
            vstate.CustomError = !String.IsNullOrEmpty(error);
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
