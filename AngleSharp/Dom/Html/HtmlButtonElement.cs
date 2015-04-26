namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Extensions;
    using AngleSharp.Html;

    /// <summary>
    /// Represents an HTML button element.
    /// </summary>
    sealed class HtmlButtonElement : HtmlFormControlElement, IHtmlButtonElement
    {
        #region Fields

        String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML button element.
        /// </summary>
        public HtmlButtonElement(Document owner, String prefix = null)
            : base(owner, Tags.Button, prefix)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the behavior of the button.
        /// </summary>
        public String Type
        {
            get { return (GetOwnAttribute(AttributeNames.Type) ?? InputTypeNames.Submit).ToLower(); }
            set { SetOwnAttribute(AttributeNames.Type, value); }
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
            get { if (Form == null) return String.Empty; return Form.Method; }
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

        #region Methods

        public override void DoClick()
        {
            if (IsClickedCancelled())
                return;

            var type = Type;
            var form = Form;

            if (type == InputTypeNames.Submit && form != null)
                form.Submit();
            else if (type == InputTypeNames.Reset && form != null)
                form.Reset();
        }

        #endregion

        #region Helper

        protected override Boolean CanBeValidated()
        {
            return Type == InputTypeNames.Submit && this.HasDataListAncestor() == false;
        }

        /// <summary>
        /// Constucts the data set (called from a form).
        /// </summary>
        /// <param name="dataSet">The dataset to construct.</param>
        /// <param name="submitter">The given submitter.</param>
        internal override void ConstructDataSet(FormDataSet dataSet, HtmlElement submitter)
        {
            if (this == submitter)
                return;

            var type = Type;

            if (type == InputTypeNames.Submit || type == InputTypeNames.Reset)
                dataSet.Append(Name, Value, Type.ToString());
        }

        #endregion
    }
}
