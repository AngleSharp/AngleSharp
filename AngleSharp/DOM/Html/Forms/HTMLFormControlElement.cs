namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the base class for all HTML form control elements.
    /// </summary>
    abstract class HTMLFormControlElement : HTMLElement, ILabelabelElement, IValidation
    {
        #region Fields

        readonly NodeList _labels;
        readonly ValidityState _vstate;
        String _error;

        #endregion

        #region ctor

        internal HTMLFormControlElement(String name, NodeFlags flags = NodeFlags.None)
            : base(name, flags | NodeFlags.Special)
        {
            _vstate = new ValidityState();
            _labels = new NodeList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        public IHtmlFormElement Form
        {
            get { return GetAssignedForm(); }
        }

        /// <summary>
        /// Gets or sets if the element is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { return GetAttribute(AttributeNames.Disabled) != null; }
            set { SetAttribute(AttributeNames.Disabled, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the autofocus HTML attribute, which indicates whether the
        /// control should have input focus when the page loads.
        /// </summary>
        public Boolean Autofocus
        {
            get { return GetAttribute(AttributeNames.AutoFocus) != null; }
            set { SetAttribute(AttributeNames.AutoFocus, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets if labels are supported.
        /// </summary>
        public Boolean SupportsLabels
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        public INodeList Labels
        {
            get { return _labels; }
        }

        /// <summary>
        /// Gets the current validation message.
        /// </summary>
        public String ValidationMessage
        {
            get { return _vstate.IsCustomError ? _error : String.Empty; }
        }

        /// <summary>
        /// Gets a value if the current element validates.
        /// </summary>
        public Boolean WillValidate
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the current validation state of the current element.
        /// </summary>
        public IValidityState Validity
        {
            get { return _vstate; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks the validity of the current element.
        /// </summary>
        /// <returns>True.</returns>
        public Boolean CheckValidity()
        {
            _vstate.Reset();
            Check(_vstate);
            return _vstate.IsValid;
        }

        /// <summary>
        /// Sets a custom validation error. If this is not the empty string,
        /// then the element is suffering from a custom validation error.
        /// </summary>
        /// <param name="error"></param>
        public void SetCustomValidity(String error)
        {
            _vstate.IsCustomError = !String.IsNullOrEmpty(error);
            _error = error;
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
