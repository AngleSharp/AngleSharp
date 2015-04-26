namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Dom.Io;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Html.InputTypes;

    /// <summary>
    /// Represents an HTML input element.
    /// </summary>
    sealed class HtmlInputElement : HtmlTextFormControlElement, IHtmlInputElement
    {
        #region Fields

        BaseInputType _type;
        Boolean? _checked;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML input element.
        /// </summary>
        public HtmlInputElement(Document owner, String prefix = null)
            : base(owner, Tags.Input, prefix, NodeFlags.SelfClosing)
        {
            RegisterAttributeObserver(AttributeNames.Type, UpdateType);
            UpdateType(null);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default value of the input field.
        /// </summary>
        public override String DefaultValue
        {
            get { return GetOwnAttribute(AttributeNames.Value) ?? String.Empty; }
            set { SetOwnAttribute(AttributeNames.Value, value); }
        }

        /// <summary>
        /// Gets or sets
        /// </summary>
        public Boolean IsDefaultChecked
        {
            get { return GetOwnAttribute(AttributeNames.Checked) != null; }
            set { SetOwnAttribute(AttributeNames.Checked, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the input element is checked or not.
        /// </summary>
        public Boolean IsChecked
        {
            get { return _checked.HasValue ? _checked.Value : IsDefaultChecked; }
            set { _checked = value; }
        }

        /// <summary>
        /// Gets or sets the type of the input field.
        /// </summary>
        public String Type
        {
            get { return _type.Name; }
            set { SetOwnAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets if the state if indeterminate.
        /// </summary>
        public Boolean IsIndeterminate 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the multiple HTML attribute, whichindicates whether multiple items can be selected.
        /// </summary>
        public Boolean IsMultiple
        {
            get { return GetOwnAttribute(AttributeNames.Multiple) != null; }
            set { SetOwnAttribute(AttributeNames.Multiple, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the value of the element, interpreted as a date, or null
        /// if conversion is not possible.
        /// </summary>
        public DateTime? ValueAsDate
        {
            get { return _type.ConvertToDate(Value); }
            set
            {
                if (value == null)
                    Value = String.Empty;
                else
                    Value = _type.ConvertFromDate(value.Value);
            }
        }

        /// <summary>
        /// Gets or sets the value of the element, interpreted as one of the following in order:
        /// 1.) Time value 2.) Number 3.) otherwise NaN.
        /// </summary>
        public Double ValueAsNumber
        {
            get { return _type.ConvertToNumber(Value) ?? Double.NaN; }
            set
            {
                if (Double.IsInfinity(value))
                    throw new DomException(DomError.TypeMismatch);
                else if (Double.IsNaN(value))
                    Value = String.Empty;
                else
                    Value = _type.ConvertFromNumber(value);
            }
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
        /// Gets or sets the accept HTML attribute, containing comma-separated list of
        /// file types accepted by the server when type is file.
        /// </summary>
        public String Accept
        {
            get { return GetOwnAttribute(AttributeNames.Accept); }
            set { SetOwnAttribute(AttributeNames.Accept, value); }
        }

        /// <summary>
        /// Gets or sets the alignment of the element.
        /// </summary>
        public Alignment Align
        {
            get { return GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Left); }
            set { SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the alt HTML attribute, containing alternative
        /// text to use when type is image.
        /// </summary>
        public String AlternativeText
        {
            get { return GetOwnAttribute(AttributeNames.Alt); }
            set { SetOwnAttribute(AttributeNames.Alt, value); }
        }

        /// <summary>
        /// Gets or sets the autocomplete HTML attribute, indicating whether
        /// the value of the control can be automatically completed by the
        /// browser. Ignored if the value of the type attribute is hidden,
        /// checkbox, radio, file, or a button type (button, submit, reset,
        /// image).
        /// </summary>
        public String Autocomplete
        {
            get { return GetOwnAttribute(AttributeNames.AutoComplete); }
            set { SetOwnAttribute(AttributeNames.AutoComplete, value); }
        }

        /// <summary>
        /// Gets a list of selected files.
        /// </summary>
        public IFileList Files
        {
            get
            {
                var type = _type as FileInputType;

                if (type != null)
                    return type.Files;

                return null;
            }
        }

        /// <summary>
        /// Gets the datalist element in the same document.
        /// Only options that are valid values for this input element will
        /// be displayed. This attribute is ignored when the type
        /// attribute's value is hidden, checkbox, radio, file, or a button type.
        /// </summary>
        public IHtmlDataListElement List
        {
            get 
            {
                var owner = Owner;

                if (owner != null)
                    return owner.GetElementById(GetOwnAttribute(AttributeNames.List)) as IHtmlDataListElement; 

                return null;
            }
        }

        /// <summary>
        /// Gets or sets max HTML attribute, containing the maximum (numeric
        /// or date-time) value for this item, which must not be less than its
        /// minimum (min attribute) value.
        /// </summary>
        public String Maximum
        {
            get { return GetOwnAttribute(AttributeNames.Max); }
            set { SetOwnAttribute(AttributeNames.Max, value); }
        }

        /// <summary>
        /// Gets or sets the min HTML attribute, containing the minimum (numeric
        /// or date-time) value for this item, which must not be greater than its
        /// maximum (max attribute) value.
        /// </summary>
        public String Minimum
        {
            get { return GetOwnAttribute(AttributeNames.Min); }
            set { SetOwnAttribute(AttributeNames.Min, value); }
        }

        /// <summary>
        /// Gets or sets the pattern HTML attribute, containing a regular expression
        /// that the control's value is checked against. The pattern must match the
        /// entire value, not just some subset. This attribute applies when the value
        /// of the type attribute is text, search, tel, url or email; otherwise it is ignored.
        /// </summary>
        public String Pattern
        {
            get { return GetOwnAttribute(AttributeNames.Pattern); }
            set { SetOwnAttribute(AttributeNames.Pattern, value); }
        }

        /// <summary>
        /// Gets or sets the size HTML attribute, containing size of the control. This value
        /// is in pixels unless the value of type is text or password, in which case, it is
        /// an integer number of characters. Applies only when type is set to text, search, tel,
        /// url, email, or password; otherwise it is ignored.
        /// </summary>
        public Int32 Size
        {
            get { return GetOwnAttribute(AttributeNames.Size).ToInteger(20); }
            set { SetOwnAttribute(AttributeNames.Size, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the src HTML attribute, which specifies a URI for the location of an
        /// image to display on the graphical submit button, if the value of type is image;
        /// otherwise it is ignored.
        /// </summary>
        public String Source
        {
            get { return GetOwnAttribute(AttributeNames.Src); }
            set { SetOwnAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the step HTML attribute, which works with min and max to limit the
        /// increments at which a numeric or date-time value can be set. It can be the string
        /// any or a positive floating point number. If this is not set to any, the control
        /// accepts only values at multiples of the step value greater than the minimum.
        /// </summary>
        public String Step
        {
            get { return GetOwnAttribute(AttributeNames.Step); }
            set { SetOwnAttribute(AttributeNames.Step, value); }
        }

        /// <summary>
        /// Gets or sets a client-side image map.
        /// </summary>
        public String UseMap
        {
            get { return GetOwnAttribute(AttributeNames.UseMap); }
            set { SetOwnAttribute(AttributeNames.UseMap, value); }
        }

        /// <summary>
        /// Gets or sets the width HTML attribute, which defines the width of the image
        /// displayed for the button, if the value of type is image.
        /// </summary>
        public Int32 DisplayWidth
        {
            get { return GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth); }
            set { SetOwnAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the height HTML attribute, which defines the
        /// height of the image displayed for the button, if the value
        /// of type is image.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight); }
            set { SetOwnAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public Int32 OriginalWidth
        {
            get
            {
                var type = _type as ImageInputType;

                if (type != null)
                    return type.Width;

                return 0;
            }
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public Int32 OriginalHeight
        {
            get
            {
                var type = _type as ImageInputType;

                if (type != null)
                    return type.Height;

                return 0;
            }
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
            {
                form.Submit();
            }
            else if (type == InputTypeNames.Reset && form != null)
            {
                form.Reset();
            }
        }

        public sealed override INode Clone(Boolean deep = true)
        {
            var node = (HtmlInputElement)base.Clone(deep);
            node._checked = _checked;
            node.UpdateType(_type.Name);
            return node;
        }

        internal override FormControlState SaveControlState()
        {
            return new FormControlState(Name, Type, Value);
        }

        internal override void RestoreFormControlState(FormControlState state)
        {
            if (state.Type == Type && state.Name == Name)
                Value = state.Value;
        }

        /// <summary>
        /// Increments the value by (step * n), where n defaults to 1 if not specified.
        /// </summary>
        /// <param name="n">Optional: The number of steps to take.</param>
        public void StepUp(Int32 n = 1)
        {
            _type.DoStep(n);
        }

        /// <summary>
        /// Decrements the value by (step * n), where n defaults to 1 if not specified. 
        /// </summary>
        /// <param name="n">Optional: The number of steps to take.</param>
        public void StepDown(Int32 n = 1)
        {
            _type.DoStep(-n);
        }

        #endregion

        #region Internal Properties

        internal Boolean IsMutable 
        {
            get { return !IsDisabled && !IsReadOnly; }
        }

        #endregion

        #region Internal Methods

        void UpdateType(String type)
        {
            _type = Factory.InputTypes.Create(this, type);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Constructs the data set (called from a form).
        /// </summary>
        /// <param name="dataSet">The dataset to construct.</param>
        /// <param name="submitter">The given submitter.</param>
        internal override void ConstructDataSet(FormDataSet dataSet, HtmlElement submitter)
        {
            _type.ConstructDataSet(dataSet);
        }

        /// <summary>
        /// Resets the form control to its initial value.
        /// </summary>
        internal override void Reset()
        {
            base.Reset();
            _checked = null;
            UpdateType(Type);
        }

        /// <summary>
        /// Checks the form control for validity.
        /// </summary>
        /// <param name="state">The element's validity state tracker.</param>
        protected override void Check(ValidityState state)
        {
            base.Check(state);
            _type.Check(state);
        }

        protected override Boolean CanBeValidated()
        {
            return _type.CanBeValidated && base.CanBeValidated();
        }

        #endregion
    }
}
