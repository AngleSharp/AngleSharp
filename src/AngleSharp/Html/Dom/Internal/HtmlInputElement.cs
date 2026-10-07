namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Html.InputTypes;
    using AngleSharp.Io.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents an HTML input element.
    /// </summary>
    sealed class HtmlInputElement : HtmlTextFormControlElement, IHtmlInputElement
    {
        #region Fields

        private static readonly ConditionalWeakTable<Document, InputCollection> _inputs = new();
        private BaseInputType? _type;
        private Boolean? _checked;
        private Boolean _uncheckedByGroup;

        #endregion

        #region ctor

        public HtmlInputElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Input, prefix, NodeFlags.SelfClosing)
        {
            _inputs.GetOrCreateValue(owner).Add(this);
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
            get => _checked ?? (IsDefaultChecked && !_uncheckedByGroup);
            set
            {
                _checked = value;
                UpdateRadioGroup();
            }
        }

        public String Type
        {
            get => _type!.Name;
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
            get => _type!.ConvertToDate(Value);
            set
            {
                if (value is null)
                {
                    Value = String.Empty;
                }
                else
                {
                    Value = _type!.ConvertFromDate(value.Value);
                }
            }
        }

        public Double ValueAsNumber
        {
            get => _type!.ConvertToNumber(Value) ?? Double.NaN;
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
                    Value = _type!.ConvertFromNumber(value);
                }
            }
        }

        public String? FormAction
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

        [AllowNull]
        public String FormTarget
        {
            get => this.GetOwnAttribute(AttributeNames.FormTarget) ?? String.Empty;
            set => this.SetOwnAttribute(AttributeNames.FormTarget, value);
        }

        public String? Accept
        {
            get => this.GetOwnAttribute(AttributeNames.Accept);
            set => this.SetOwnAttribute(AttributeNames.Accept, value);
        }

        public Alignment Align
        {
            get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Left);
            set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
        }

        public String? AlternativeText
        {
            get => this.GetOwnAttribute(AttributeNames.Alt);
            set => this.SetOwnAttribute(AttributeNames.Alt, value);
        }

        public String? Autocomplete
        {
            get => this.GetOwnAttribute(AttributeNames.AutoComplete);
            set => this.SetOwnAttribute(AttributeNames.AutoComplete, value);
        }

        public IFileList? Files
        {
            get
            {
                var type = _type as FileInputType;
                return type?.Files;
            }
        }

        public IHtmlDataListElement? List
        {
            get
            {
                var list = this.GetOwnAttribute(AttributeNames.List);

                if (list is { Length: > 0 })
                {
                    var element = Owner?.GetElementById(list);
                    return element as IHtmlDataListElement;
                }

                return null;
            }
        }

        public String? Maximum
        {
            get => this.GetOwnAttribute(AttributeNames.Max);
            set => this.SetOwnAttribute(AttributeNames.Max, value);
        }

        public String? Minimum
        {
            get => this.GetOwnAttribute(AttributeNames.Min);
            set => this.SetOwnAttribute(AttributeNames.Min, value);
        }

        public String? Pattern
        {
            get => this.GetOwnAttribute(AttributeNames.Pattern);
            set => this.SetOwnAttribute(AttributeNames.Pattern, value);
        }

        public Int32 Size
        {
            get => this.GetOwnAttribute(AttributeNames.Size).ToInteger(20);
            set => this.SetOwnAttribute(AttributeNames.Size, value.ToString());
        }

        public String? Source
        {
            get => this.GetOwnAttribute(AttributeNames.Src);
            set => this.SetOwnAttribute(AttributeNames.Src, value);
        }

        public String? Step
        {
            get => this.GetOwnAttribute(AttributeNames.Step);
            set => this.SetOwnAttribute(AttributeNames.Step, value);
        }

        public String? UseMap
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
            node._uncheckedByGroup = _uncheckedByGroup;
            node.IsIndeterminate = IsIndeterminate;
            node.UpdateType(_type!.Name);
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
            new(Name!, Type, Value);

        internal override void RestoreFormControlState(FormControlState state)
        {
            if (state.Type.Is(Type) && state.Name.Is(Name))
            {
                Value = state.Value;
            }
        }

        public void StepUp(Int32 n = 1) => _type!.DoStep(n);

        public void StepDown(Int32 n = 1) => _type!.DoStep(-n);

        /// <inheritdoc />
        public override void DoFocus()
        {
            // A hidden input is never focusable, matching how a real browser treats it (it is not
            // even rendered) - every other type defers to the shared text-form-control behavior
            // (disabled-state check, then IsFocused = true).
            if (!Type.Is(InputTypeNames.Hidden))
            {
                base.DoFocus();
            }
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();
            var type = this.GetOwnAttribute(AttributeNames.Type);
            UpdateType(type!);
            UpdateRadioGroup();
        }

        internal void UpdateType(String value) =>
            _type = Context.GetFactory<IInputTypeFactory>().Create(this, value);

        internal void UpdateCheckedness()
        {
            if (_checked is null)
            {
                _uncheckedByGroup = false;
                UpdateRadioGroup();
            }
        }

        #endregion

        #region Helpers

        internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
        {
            if (_type!.IsAppendingData(submitter))
            {
                _type.ConstructDataSet(dataSet);
            }
        }

        internal override void Reset()
        {
            base.Reset();
            _checked = null;
            _uncheckedByGroup = false;
            UpdateType(Type);
            UpdateRadioGroup();
        }

        protected override void NodeIsAdopted(Document oldDocument)
        {
            _inputs.GetOrCreateValue(oldDocument).Remove(this);
            _inputs.GetOrCreateValue(Owner).Add(this);
        }

        private void UpdateRadioGroup()
        {
            var name = Name;

            if (IsChecked && _type?.Name == InputTypeNames.Radio && !String.IsNullOrEmpty(name))
            {
                var form = Form;
                var root = RadioGroupRoot;
                var inputs = _inputs.GetOrCreateValue(Owner).Items;

                for (var i = 0; i < inputs.Count; i++)
                {
                    if (inputs[i].TryGetTarget(out var other) && !ReferenceEquals(this, other) &&
                        other._type?.Name == InputTypeNames.Radio && other.IsChecked &&
                        String.Equals(name, other.Name, StringComparison.Ordinal) &&
                        ReferenceEquals(root, other.RadioGroupRoot) && ReferenceEquals(form, other.Form))
                    {
                        if (other._checked.HasValue)
                        {
                            other._checked = false;
                        }
                        else
                        {
                            other._uncheckedByGroup = true;
                        }
                    }
                }
            }
        }

        // During parsing, template children have not yet moved into the content fragment.
        private INode RadioGroupRoot =>
            this.GetAncestor<HtmlTemplateElement>() is { IsParsingContent: true } template ? template : this.GetRoot();

        private sealed class InputCollection
        {
            private Int32 _cleanupThreshold = 64;

            public InputCollection()
            {
                Items = new List<WeakReference<HtmlInputElement>>();
            }

            public List<WeakReference<HtmlInputElement>> Items { get; }

            public void Add(HtmlInputElement input)
            {
                if (Items.Count >= _cleanupThreshold)
                {
                    var kept = 0;

                    for (var i = 0; i < Items.Count; i++)
                    {
                        if (Items[i].TryGetTarget(out _))
                        {
                            Items[kept++] = Items[i];
                        }
                    }

                    Items.RemoveRange(kept, Items.Count - kept);
                    _cleanupThreshold = Math.Max(64, kept * 2);
                }

                Items.Add(new WeakReference<HtmlInputElement>(input));
            }

            public void Remove(HtmlInputElement input)
            {
                for (var i = 0; i < Items.Count; i++)
                {
                    if (Items[i].TryGetTarget(out var item) && ReferenceEquals(item, input))
                    {
                        Items.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        protected override void Check(ValidityState state)
        {
            base.Check(state);
            var result = _type!.Check(state);
            state.Reset(result);
        }

        protected override Boolean CanBeValidated() =>
            _type!.CanBeValidated && base.CanBeValidated();

        #endregion
    }
}
