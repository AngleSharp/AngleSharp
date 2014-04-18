namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represents the base class for all HTML form control elements.
    /// </summary>
    public abstract class HTMLFormControlElement : HTMLElement, ILabelabelElement, IValidation
    {
        #region Fields

        NodeList _labels;
        ValidityState _vstate;
        String _error;

        #endregion

        #region ctor

        internal HTMLFormControlElement()
        {
            _vstate = new ValidityState();
            _labels = new NodeList();
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

        /// <summary>
        /// Gets or sets the autofocus HTML attribute, which indicates whether the
        /// control should have input focus when the page loads.
        /// </summary>
        [DOM("autofocus")]
        public Boolean Autofocus
        {
            get { return GetAttribute("autofocus") != null; }
            set { SetAttribute("autofocus", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets if labels are supported.
        /// </summary>
        [DOM("supportsLabels")]
        public Boolean SupportsLabels
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        [DOM("labels")]
        public NodeList Labels
        {
            get { return _labels; }
        }

        /// <summary>
        /// Gets the current validation message.
        /// </summary>
        [DOM("validationMessage")]
        public String ValidationMessage
        {
            get { return _vstate.CustomError ? _error : String.Empty; }
        }

        /// <summary>
        /// Gets a value if the current element validates.
        /// </summary>
        [DOM("willValidate")]
        public Boolean WillValidate
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the current validation state of the current element.
        /// </summary>
        [DOM("validity")]
        public ValidityState Validity
        {
            get { return _vstate; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks the validity of the current element.
        /// </summary>
        /// <returns>True.</returns>
        [DOM("checkValidity")]
        public Boolean CheckValidity()
        {
            Check(_vstate);
            return _vstate.Valid;
        }

        /// <summary>
        /// Sets a custom validation error. If this is not the empty string,
        /// then the element is suffering from a custom validation error.
        /// </summary>
        /// <param name="error"></param>
        [DOM("setCustomValidity")]
        public void SetCustomValidity(String error)
        {
            _vstate.CustomError = !String.IsNullOrEmpty(error);
            this._error = error;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Constucts the data set (called from a form).
        /// </summary>
        /// <param name="dataSet">The dataset to construct.</param>
        /// <param name="submitter">The given submitter.</param>
        internal virtual void ConstructDataSet(FormDataSet dataSet, HTMLElement submitter)
        { }

        /// <summary>
        /// Resets the form control to its initial value.
        /// </summary>
        internal virtual void Reset()
        { }

        /// <summary>
        /// Checks the form control for validity.
        /// </summary>
        /// <param name="state">The element's validity state tracker.</param>
        protected virtual void Check(ValidityState state)
        { }

        #endregion
    }
}
