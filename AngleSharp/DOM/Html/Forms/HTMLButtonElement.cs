namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents an HTML button element.
    /// </summary>
    sealed class HTMLButtonElement : HTMLFormControlElement, IHtmlButtonElement
    {
        #region Fields

        String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML button element.
        /// </summary>
        internal HTMLButtonElement()
            : base(Tags.Button)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the behavior of the button.
        /// </summary>
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets the URI of a resource that processes information submitted by the button.
        /// If specified, this attribute overrides the action attribute of the form element that owns this element.
        /// </summary>
        public String FormAction
        {
            get { if (Form == null) return String.Empty; return Form.Action; }
            set { if (Form != null) Form.Action = value; }
        }

        /// <summary>
        /// Gets or sets the type of content that is used to submit the form to the server. If specified, this
        /// attribute overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        public String FormEncType
        {
            get { if (Form == null) return String.Empty; return Form.Enctype; }
            set { if (Form != null) Form.Enctype = value; }
        }

        /// <summary>
        /// Gets or sets the HTTP method that the browser uses to submit the form. If specified, this attribute
        /// overrides the method attribute of the form element that owns this element.
        /// </summary>
        public String FormMethod
        {
            get { if (Form == null) return "post"; return Form.Method; }
            set { if (Form != null) Form.Method = value; }
        }

        /// <summary>
        /// Gets or sets that the form is not to be validated when it is submitted. If specified, this attribute
        /// overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        public Boolean FormNoValidate
        {
            get { if (Form == null) return false; return Form.NoValidate; }
            set { if (Form != null) Form.NoValidate = value; }
        }

        /// <summary>
        /// Gets or sets A name or keyword indicating where to display the response that is received after submitting
        /// the form. If specified, this attribute overrides the target attribute of the form element that owns this element.
        /// </summary>
        public String FormTarget
        {
            get { if (Form == null) return String.Empty; return Form.Target; }
            set { if (Form != null) Form.Target = value; }
        }

        /// <summary>
        /// Gets or sets the current value of the control.
        /// </summary>
        public String Value
        {
            get { return _value ?? String.Empty; }
            set { _value = value; }
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

        #region Helper

        /// <summary>
        /// Constucts the data set (called from a form).
        /// </summary>
        /// <param name="dataSet">The dataset to construct.</param>
        /// <param name="submitter">The given submitter.</param>
        internal override void ConstructDataSet(FormDataSet dataSet, HTMLElement submitter)
        {
            if (this == submitter)
                return;

            var type = Type.ToEnum(ButtonType.Submit);

            if (type == ButtonType.Submit || type == ButtonType.Reset)
                dataSet.Append(Name, Value, Type.ToString());
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// An enumeration with possible input types.
        /// </summary>
        public enum ButtonType : ushort
        {
            /// <summary>
            /// The button submits the form.
            /// </summary>
            Submit,
            /// <summary>
            /// The button resets the form.
            /// </summary>
            Reset,
            /// <summary>
            /// The button does nothing.
            /// </summary>
            Button,
            /// <summary>
            /// The button displays a menu.
            /// </summary>
            Menu
        }

        #endregion
    }
}
