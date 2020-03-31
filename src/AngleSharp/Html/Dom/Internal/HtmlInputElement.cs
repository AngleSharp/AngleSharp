namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Html.InputTypes;
    using AngleSharp.Io.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents an HTML input element.
    /// </summary>
    sealed class HtmlInputElement : HtmlTextFormControlElement, IHtmlInputElement
    {
        #region Fields

        private BaseInputType _type;
        private Boolean? _checked;

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
            get => this.GetOwnAttribute(AttributeNames.Value) ?? String.Empty;
            set => this.SetOwnAttribute(AttributeNames.Value, value);
        }

        public Boolean IsDefaultChecked
        {
            get => this.GetBoolAttribute(AttributeNames.Checked);
            set => this.SetBoolAttribute(AttributeNames.Checked, value);
        }

        public Boolean IsChecked
        {
            get => _checked.HasValue ? _checked.Value : IsDefaultChecked;
            set => _checked = value;
        }

        public String Type
        {
            get => _type.Name;
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        public Boolean IsIndeterminate
        {
            get;
            set;
        }

        public Boolean IsMultiple
        {
            get => this.GetBoolAttribute(AttributeNames.Multiple);
            set => this.SetBoolAttribute(AttributeNames.Multiple, value);
        }

        public DateTime? ValueAsDate
        {
            get => _type.ConvertToDate(Value);
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
            get => _type.ConvertToNumber(Value) ?? Double.NaN;
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
            get => this.GetOwnAttribute(AttributeNames.FormAction) ?? Owner?.DocumentUri;
            set => this.SetOwnAttribute(AttributeNames.FormAction, value);
        }

        public String FormEncType
        {
            get => this.GetOwnAttribute(AttributeNames.FormEncType).ToEncodingType() ?? String.Empty;
            set => this.SetOwnAttribute(AttributeNames.FormEncType, value);
        }

        public String FormMethod
        {
            get => this.GetOwnAttribute(AttributeNames.FormMethod).ToFormMethod() ?? String.Empty;
            set => this.SetOwnAttribute(AttributeNames.FormMethod, value);
        }

        public Boolean FormNoValidate
        {
            get => this.GetBoolAttribute(AttributeNames.FormNoValidate);
            set => this.SetBoolAttribute(AttributeNames.FormNoValidate, value);
        }

        public String FormTarget
        {
            get => this.GetOwnAttribute(AttributeNames.FormTarget) ?? String.Empty;
            set => this.SetOwnAttribute(AttributeNames.FormTarget, value);
        }

        public String Accept
        {
            get => this.GetOwnAttribute(AttributeNames.Accept);
            set => this.SetOwnAttribute(AttributeNames.Accept, value);
        }

        public Alignment Align
        {
            get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Left);
            set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
        }

        public String AlternativeText
        {
            get => this.GetOwnAttribute(AttributeNames.Alt);
            set => this.SetOwnAttribute(AttributeNames.Alt, value);
        }

        public String Autocomplete
        {
            get => this.GetOwnAttribute(AttributeNames.AutoComplete);
            set => this.SetOwnAttribute(AttributeNames.AutoComplete, value);
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
            get => this.GetOwnAttribute(AttributeNames.Max);
            set => this.SetOwnAttribute(AttributeNames.Max, value);
        }

        public String Minimum
        {
            get => this.GetOwnAttribute(AttributeNames.Min);
            set => this.SetOwnAttribute(AttributeNames.Min, value);
        }

        public String Pattern
        {
            get => this.GetOwnAttribute(AttributeNames.Pattern);
            set => this.SetOwnAttribute(AttributeNames.Pattern, value);
        }

        public Int32 Size
        {
            get => this.GetOwnAttribute(AttributeNames.Size).ToInteger(20);
            set => this.SetOwnAttribute(AttributeNames.Size, value.ToString());
        }

        public String Source
        {
            get => this.GetOwnAttribute(AttributeNames.Src);
            set => this.SetOwnAttribute(AttributeNames.Src, value);
        }

        public String Step
        {
            get => this.GetOwnAttribute(AttributeNames.Step);
            set => this.SetOwnAttribute(AttributeNames.Step, value);
        }

        public String UseMap
        {
            get => this.GetOwnAttribute(AttributeNames.UseMap);
            set => this.SetOwnAttribute(AttributeNames.UseMap, value);
        }

        public Int32 DisplayWidth
        {
            get => this.GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth);
            set => this.SetOwnAttribute(AttributeNames.Width, value.ToString());
        }

        public Int32 DisplayHeight
        {
            get => this.GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight);
            set => this.SetOwnAttribute(AttributeNames.Height, value.ToString());
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

        public sealed override Node Clone(Document owner, Boolean deep)
        {
            var node = (HtmlInputElement)base.Clone(owner, deep);
            node._checked = _checked;
            node.UpdateType(_type.Name);
            return node;
        }

        public override async void DoClick()
        {
            var cancelled = await IsClickedCancelled().ConfigureAwait(false);
            var form = Form;

            if (!cancelled && form != null)
            {
                var type = Type;

                if (type.Is(InputTypeNames.Submit))
                {
                    await form.SubmitAsync().ConfigureAwait(false);
                }
                else if (type.Is(InputTypeNames.Reset))
                {
                    form.Reset();
                }
            }
        }

        internal override FormControlState SaveControlState() =>
            new FormControlState(Name, Type, Value);

        internal override void RestoreFormControlState(FormControlState state)
        {
            if (state.Type.Is(Type) && state.Name.Is(Name))
            {
                Value = state.Value;
            }
        }

        public void StepUp(Int32 n = 1) => _type.DoStep(n);

        public void StepDown(Int32 n = 1) => _type.DoStep(-n);

        #endregion

        #region Internal Properties

        internal Boolean IsMutable => !IsDisabled && !IsReadOnly;

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();
            var type = this.GetOwnAttribute(AttributeNames.Type);
            UpdateType(type);
        }

        internal void UpdateType(String value) =>
            _type = Context.GetFactory<IInputTypeFactory>().Create(this, value);

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
            var result = _type.Check(state);
            state.Reset(result);
        }

        protected override Boolean CanBeValidated() =>
            _type.CanBeValidated && base.CanBeValidated();

        #endregion
    }
}
