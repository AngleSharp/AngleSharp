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

        static HtmlInputElement()
        {
            RegisterCallback<HtmlInputElement>(AttributeNames.Type, (element, value) => element.UpdateType(value));
        }

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
            get { return this.HasOwnAttribute(AttributeNames.Checked); }
            set { this.SetOwnAttribute(AttributeNames.Checked, value ? String.Empty : null); }
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
            get { return this.HasOwnAttribute(AttributeNames.Multiple); }
            set { this.SetOwnAttribute(AttributeNames.Multiple, value ? String.Empty : null); }
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
                {
                    throw new DomException(DomError.TypeMismatch);
                }

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
            get { if (Form == null) return String.Empty; return Form.Action; }
            set { if (Form != null) Form.Action = value; }
        }

        public String FormEncType
        {
            get { if (Form == null) return String.Empty; return Form.Enctype; }
            set { if (Form != null) Form.Enctype = value; }
        }

        public String FormMethod
        {
            get { if (Form == null) return String.Empty; return Form.Method; }
            set { if (Form != null) Form.Method = value; }
        }

        public Boolean FormNoValidate
        {
            get { if (Form == null) return false; return Form.NoValidate; }
            set { if (Form != null) Form.NoValidate = value; }
        }

        public String FormTarget
        {
            get { if (Form == null) return String.Empty; return Form.Target; }
            set { if (Form != null) Form.Target = value; }
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

                if (type != null)
                {
                    return type.Files;
                }

                return null;
            }
        }

        public IHtmlDataListElement List
        {
            get 
            {
                var owner = Owner;

                if (owner != null)
                {
                    var list = this.GetOwnAttribute(AttributeNames.List);
                    var element = owner.GetElementById(list);
                    return element as IHtmlDataListElement;
                }

                return null;
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

                if (type != null)
                {
                    return type.Width;
                }

                return 0;
            }
        }

        public Int32 OriginalHeight
        {
            get
            {
                var type = _type as ImageInputType;

                if (type != null)
                {
                    return type.Height;
                }

                return 0;
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
                var form = Form;

                if (type.Is(InputTypeNames.Submit) && form != null)
                {
                    form.SubmitAsync();
                }
                else if (type.Is(InputTypeNames.Reset) && form != null)
                {
                    form.Reset();
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

        void UpdateType(String type)
        {
            var factory = Owner.Options.GetFactory<IInputTypeFactory>();
            _type = factory.Create(this, type);
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
