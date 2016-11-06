namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an HTML button element.
    /// </summary>
    sealed class HtmlButtonElement : HtmlFormControlElement, IHtmlButtonElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML button element.
        /// </summary>
        public HtmlButtonElement(Document owner, String prefix = null)
            : base(owner, TagNames.Button, prefix)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the behavior of the button.
        /// </summary>
        public String Type
        {
            get { return (this.GetOwnAttribute(AttributeNames.Type) ?? InputTypeNames.Submit).ToLowerInvariant(); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets the URI of a resource that processes information submitted by the button.
        /// If specified, this attribute overrides the action attribute of the form element that owns this element.
        /// </summary>
        public String FormAction
        {
            get { var form = Form; if (form == null) return String.Empty; return form.Action; }
            set { var form = Form; if (form != null) form.Action = value; }
        }

        /// <summary>
        /// Gets or sets the type of content that is used to submit the form to the server. If specified, this
        /// attribute overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        public String FormEncType
        {
            get { var form = Form; if (form == null) return String.Empty; return form.Enctype; }
            set { var form = Form; if (form != null) form.Enctype = value; }
        }

        /// <summary>
        /// Gets or sets the HTTP method that the browser uses to submit the form. If specified, this attribute
        /// overrides the method attribute of the form element that owns this element.
        /// </summary>
        public String FormMethod
        {
            get { var form = Form; if (form == null) return String.Empty; return form.Method; }
            set { var form = Form; if (form != null) form.Method = value; }
        }

        /// <summary>
        /// Gets or sets that the form is not to be validated when it is submitted. If specified, this attribute
        /// overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        public Boolean FormNoValidate
        {
            get { var form = Form; if (form == null) return false; return form.NoValidate; }
            set { var form = Form; if (form != null) form.NoValidate = value; }
        }

        /// <summary>
        /// Gets or sets A name or keyword indicating where to display the response that is received after submitting
        /// the form. If specified, this attribute overrides the target attribute of the form element that owns this element.
        /// </summary>
        public String FormTarget
        {
            get { var form = Form; if (form == null) return String.Empty; return form.Target; }
            set { var form = Form; if (form != null) form.Target = value; }
        }

        /// <summary>
        /// Gets or sets the current value of the control.
        /// </summary>
        public String Value
        {
            get { return this.GetOwnAttribute(AttributeNames.Value) ?? String.Empty; }
            set { this.SetOwnAttribute(AttributeNames.Value, value); }
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
            var form = Form;

            if (!IsClickedCancelled() && form != null)
            {
                var type = Type;

                if (type.Is(InputTypeNames.Submit))
                {
                    form.SubmitAsync(this);
                }
                else if (type.Is(InputTypeNames.Reset))
                {
                    form.Reset();
                }
            }
        }

        #endregion

        #region Helper

        protected override Boolean CanBeValidated()
        {
            return Type.Is(InputTypeNames.Submit) && !this.HasDataListAncestor();
        }

        internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
        {
            var type = Type;

            if (Object.ReferenceEquals(this, submitter) && type.IsOneOf(InputTypeNames.Submit, InputTypeNames.Reset))
            {
                dataSet.Append(Name, Value, type);
            }
        }

        #endregion
    }
}
