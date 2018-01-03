namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Html.InputTypes;
    using AngleSharp.Services;
    using System;

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

        public HtmlInputElement(Document owner, String prefix = null)
            : base(owner, TagNames.Input, prefix, NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        public override String DefaultValue
        {
            get { return this.GetOwnAttribute(AttributeNames.Value) ?? String.Empty; }
            set { this.SetOwnAttribute(AttributeNames.Value, value); }
        }

        public Boolean IsDefaultChecked
        {
            get { return this.GetBoolAttribute(AttributeNames.Checked); }
            set { this.SetBoolAttribute(AttributeNames.Checked, value); }
        }

        public Boolean IsChecked
        {
            get { return _checked.HasValue ? _checked.Value : IsDefaultChecked; }
            set { _checked = value; }
        }

        public String Type
        {
            get { return _type.Name; }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        public Boolean IsIndeterminate 
        {
            get;
            set;
        }

        public Boolean IsMultiple
        {
            get { return this.GetBoolAttribute(AttributeNames.Multiple); }
            set { this.SetBoolAttribute(AttributeNames.Multiple, value); }
        }

        public DateTime? ValueAsDate
        {
            get { return _type.ConvertToDate(Value); }
            set
            {
                if (value == null)
                {
                    Value = String.Empty;
                }
                else
                {
                    Value = _type.ConvertFromDate(value.Value);
                }
            }
        }

        public Double ValueAsNumber
        {
            get { return _type.ConvertToNumber(Value) ?? Double.NaN; }
            set
            {
                if (Double.IsInfinity(value))
                    throw new DomException(DomError.TypeMismatch);

                if (Double.IsNaN(value))
                {
                    Value = String.Empty;
                }
                else
                {
                    Value = _type.ConvertFromNumber(value);
                }
            }
        }

        public String FormAction
        {
            get { var form = Form; if (form == null) return String.Empty; return form.Action; }
            set { var form = Form; if (form != null) form.Action = value; }
        }

        public String FormEncType
        {
            get { var form = Form; if (form == null) return String.Empty; return form.Enctype; }
            set { var form = Form; if (form != null) form.Enctype = value; }
        }

        public String FormMethod
        {
            get { var form = Form; if (form == null) return String.Empty; return form.Method; }
            set { var form = Form; if (form != null) form.Method = value; }
        }

        public Boolean FormNoValidate
        {
            get { var form = Form; if (form == null) return false; return form.NoValidate; }
            set { var form = Form; if (form != null) form.NoValidate = value; }
        }

        public String FormTarget
        {
            get { var form = Form; if (form == null) return String.Empty; return form.Target; }
            set { var form = Form; if (form != null) form.Target = value; }
        }

        public String Accept
        {
            get { return this.GetOwnAttribute(AttributeNames.Accept); }
            set { this.SetOwnAttribute(AttributeNames.Accept, value); }
        }

        public Alignment Align
        {
            get { return this.GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Left); }
            set { this.SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        public String AlternativeText
        {
            get { return this.GetOwnAttribute(AttributeNames.Alt); }
            set { this.SetOwnAttribute(AttributeNames.Alt, value); }
        }

        public String Autocomplete
        {
            get { return this.GetOwnAttribute(AttributeNames.AutoComplete); }
            set { this.SetOwnAttribute(AttributeNames.AutoComplete, value); }
        }

        public IFileList Files
        {
            get
            {
                var type = _type as FileInputType;
                return type?.Files;
            }
        }

        public IHtmlDataListElement List
        {
            get
            {
                var list = this.GetOwnAttribute(AttributeNames.List);
                var element = Owner?.GetElementById(list);
                return element as IHtmlDataListElement;
            }
        }

        public String Maximum
        {
            get { return this.GetOwnAttribute(AttributeNames.Max); }
            set { this.SetOwnAttribute(AttributeNames.Max, value); }
        }

        public String Minimum
        {
            get { return this.GetOwnAttribute(AttributeNames.Min); }
            set { this.SetOwnAttribute(AttributeNames.Min, value); }
        }

        public String Pattern
        {
            get { return this.GetOwnAttribute(AttributeNames.Pattern); }
            set { this.SetOwnAttribute(AttributeNames.Pattern, value); }
        }

        public Int32 Size
        {
            get { return this.GetOwnAttribute(AttributeNames.Size).ToInteger(20); }
            set { this.SetOwnAttribute(AttributeNames.Size, value.ToString()); }
        }

        public String Source
        {
            get { return this.GetOwnAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        public String Step
        {
            get { return this.GetOwnAttribute(AttributeNames.Step); }
            set { this.SetOwnAttribute(AttributeNames.Step, value); }
        }

        public String UseMap
        {
            get { return this.GetOwnAttribute(AttributeNames.UseMap); }
            set { this.SetOwnAttribute(AttributeNames.UseMap, value); }
        }

        public Int32 DisplayWidth
        {
            get { return this.GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth); }
            set { this.SetOwnAttribute(AttributeNames.Width, value.ToString()); }
        }

        public Int32 DisplayHeight
        {
            get { return this.GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight); }
            set { this.SetOwnAttribute(AttributeNames.Height, value.ToString()); }
        }

        public Int32 OriginalWidth
        {
            get
            {
                var type = _type as ImageInputType;
                return type?.Width ?? 0;
            }
        }

        public Int32 OriginalHeight
        {
            get
            {
                var type = _type as ImageInputType;
                return type?.Height ?? 0;
            }
        }

        #endregion

        #region Design properties

        internal Boolean IsVisited
        {
            get;
            set;
        }

        internal Boolean IsActive
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public override void DoClick()
        {
            if (!IsClickedCancelled())
            {
                var type = Type;

                if (type.Is(InputTypeNames.Submit))
                {
                    Form?.SubmitAsync();
                }
                else if (type.Is(InputTypeNames.Reset))
                {
                    Form?.Reset();
                }
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
            if (state.Type.Is(Type) && state.Name.Is(Name))
            {
                Value = state.Value;
            }
        }

        public void StepUp(Int32 n = 1)
        {
            _type.DoStep(n);
        }

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

        internal override void SetupElement()
        {
            base.SetupElement();
            var type = this.GetOwnAttribute(AttributeNames.Type);
            UpdateType(type);
        }

        internal void UpdateType(String value)
        {
            var factory = Owner.Options.GetFactory<IInputTypeFactory>();
            _type = factory.Create(this, value);
        }

        #endregion

        #region Helpers

        internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
        {
            if (_type.IsAppendingData(submitter))
            {
                _type.ConstructDataSet(dataSet);
            }
        }

        internal override void Reset()
        {
            base.Reset();
            _checked = null;
            UpdateType(Type);
        }

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
