namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Text;
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
            get => (this.GetOwnAttribute(AttributeNames.Type) ?? InputTypeNames.Submit).ToLowerInvariant();
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        /// <summary>
        /// Gets or sets the URI of a resource that processes information submitted by the button.
        /// If specified, this attribute overrides the action attribute of the form element that owns this element.
        /// </summary>
        public String FormAction
        {
            get => this.GetOwnAttribute(AttributeNames.FormAction) ?? Owner?.DocumentUri;
            set => this.SetOwnAttribute(AttributeNames.FormAction, value);
        }

        /// <summary>
        /// Gets or sets the type of content that is used to submit the form to the server. If specified, this
        /// attribute overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        public String FormEncType
        {
            get => this.GetOwnAttribute(AttributeNames.FormEncType).ToEncodingType() ?? String.Empty;
            set => this.SetOwnAttribute(AttributeNames.FormEncType, value);
        }

        /// <summary>
        /// Gets or sets the HTTP method that the browser uses to submit the form. If specified, this attribute
        /// overrides the method attribute of the form element that owns this element.
        /// </summary>
        public String FormMethod
        {
            get => this.GetOwnAttribute(AttributeNames.FormMethod).ToFormMethod() ?? String.Empty;
            set => this.SetOwnAttribute(AttributeNames.FormMethod, value);
        }

        /// <summary>
        /// Gets or sets that the form is not to be validated when it is submitted. If specified, this attribute
        /// overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        public Boolean FormNoValidate
        {
            get => this.GetBoolAttribute(AttributeNames.FormNoValidate);
            set => this.SetBoolAttribute(AttributeNames.FormNoValidate, value);
        }

        /// <summary>
        /// Gets or sets A name or keyword indicating where to display the response that is received after submitting
        /// the form. If specified, this attribute overrides the target attribute of the form element that owns this element.
        /// </summary>
        public String FormTarget
        {
            get => this.GetOwnAttribute(AttributeNames.FormTarget) ?? String.Empty;
            set => this.SetOwnAttribute(AttributeNames.FormTarget, value);
        }

        /// <summary>
        /// Gets or sets the current value of the control.
        /// </summary>
        public String Value
        {
            get => this.GetOwnAttribute(AttributeNames.Value) ?? String.Empty;
            set => this.SetOwnAttribute(AttributeNames.Value, value);
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

        public override async void DoClick()
        {
            var cancelled = await IsClickedCancelled().ConfigureAwait(false);
            var form = Form;

            if (!cancelled && form != null)
            {
                var type = Type;

                if (type.Is(InputTypeNames.Submit))
                {
                    await form.SubmitAsync(this).ConfigureAwait(false);
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
