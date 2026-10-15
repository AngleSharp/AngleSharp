namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the select element.
    /// </summary>
    sealed class HtmlSelectElement : HtmlFormControlElementWithState, IHtmlSelectElement
    {
        #region Fields

        private OptionsCollection? _options;
        private HtmlCollection<IHtmlOptionElement>? _selected;
        private HtmlOptionElement? _selectedOption;

        #endregion

        #region ctor
        
        public HtmlSelectElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Select, prefix)
        {
        }

        #endregion

        #region Index

        public IHtmlOptionElement this[Int32 index]
        {
            get => Options.GetOptionAt(index);
            set => Options.SetOptionAt(index, value);
        }

        #endregion

        #region Properties

        public Int32 Size
        {
            get => this.GetOwnAttribute(AttributeNames.Size).ToInteger(0);
            set => this.SetOwnAttribute(AttributeNames.Size, value.ToString());
        }

        public Boolean IsRequired
        {
            get => this.GetBoolAttribute(AttributeNames.Required);
            set => this.SetBoolAttribute(AttributeNames.Required, value);
        }

        public IHtmlCollection<IHtmlOptionElement> SelectedOptions => _selected ??= new HtmlCollection<IHtmlOptionElement>(Options.Where(m => m.IsSelected));

        public Int32 SelectedIndex => Options.SelectedIndex;

        public String? Value
        {
            get
            {
                var options = Options;

                foreach (var option in options)
                {
                    if (option.IsSelected)
                    {
                        return option.Value;
                    }
                }

                return null;
            }
            set => UpdateValue(value!);
        }

        public Int32 Length => Options.Length;

        public Boolean IsMultiple
        {
            get => this.GetBoolAttribute(AttributeNames.Multiple);
            set => this.SetBoolAttribute(AttributeNames.Multiple, value);
        }

        public IHtmlOptionsCollection Options => _options ??= new OptionsCollection(this);

        public String Type => IsMultiple ? InputTypeNames.SelectMultiple : InputTypeNames.SelectOne;

        #endregion

        #region Methods

        public void AddOption(IHtmlOptionElement element, IHtmlElement? before = null)
        {
            Options.Add(element, before);
        }

        public void AddOption(IHtmlOptionsGroupElement element, IHtmlElement? before = null)
        {
            Options.Add(element, before);
        }

        public void RemoveOptionAt(Int32 index)
        {
            Options.Remove(index);
        }

        /// <inheritdoc />
        public override void DoFocus()
        {
            if (!IsDisabled)
            {
                IsFocused = true;
            }
        }

        /// <inheritdoc />
        public override void DoBlur()
        {
            if (IsFocused)
            {
                IsFocused = false;
            }
        }

        #endregion

        #region Internal Methods

        internal override FormControlState SaveControlState()
        {
            return new FormControlState(Name!, Type, Value);
        }

        internal override void RestoreFormControlState(FormControlState state)
        {
            if (state.Type.Is(Type) && state.Name.Is(Name))
            {
                Value = state.Value;
            }
        }

        internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
        {
            foreach (var option in Options)
            {
                if (option.IsSelected && !option.IsDisabled &&
                    !(option.ParentElement is IHtmlOptionsGroupElement group && group.IsDisabled))
                {
                    dataSet.Append(Name!, option.Value, Type);
                }
            }
        }

        /// <summary>
        /// Normalizes the current option list after a reset-triggering mutation, without
        /// changing option dirtiness. Getters and submission must not run this algorithm.
        /// </summary>
        internal void NormalizeSelectedness()
        {
            if (IsMultiple)
            {
                return;
            }

            if (_selectedOption is { IsSelected: true } current && current.SelectionOwner == this)
            {
                return;
            }

            _selectedOption = null;

            if (Size <= 1)
            {
                foreach (var item in Options)
                {
                    var option = (HtmlOptionElement)item;

                    if (!option.IsDisabled &&
                        !(option.ParentElement is IHtmlOptionsGroupElement owner && owner.IsDisabled))
                    {
                        option.SetSelectedness(true);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Tracks the single selected option so insertion need not rescan every existing option.
        /// Clearing or moving that option also releases the reference held by its old select.
        /// </summary>
        internal void UpdateSelectedOption(HtmlOptionElement option, Boolean selected)
        {
            if (selected && !IsMultiple)
            {
                var previous = _selectedOption;
                _selectedOption = option;

                if (previous != option && previous is not null && previous.SelectionOwner == this)
                {
                    previous.SetSelectedness(false);
                }
            }
            else if (_selectedOption == option)
            {
                _selectedOption = null;
            }
        }

        /// <summary>
        /// Registers inserted options before applying the single-select fallback.
        /// Fragment insertion calls this in order after all children are attached.
        /// </summary>
        internal void UpdateOptions(Node node, Boolean inserted)
        {
            if (node is HtmlOptionElement option)
            {
                if (inserted && option.IsSelected)
                {
                    UpdateSelectedOption(option, true);
                }
            }
            else if (node is HtmlOptionsGroupElement group)
            {
                var hasOptions = false;

                foreach (var child in group.ChildNodes)
                {
                    if (child is HtmlOptionElement item)
                    {
                        hasOptions = true;

                        if (inserted && item.IsSelected)
                        {
                            UpdateSelectedOption(item, true);
                        }
                    }
                }

                if (!hasOptions)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            NormalizeSelectedness();
        }

        protected override void NodeIsInserted(Node newNode)
        {
            base.NodeIsInserted(newNode);
            UpdateOptions(newNode, true);
        }

        protected override void NodeIsRemoved(Node removedNode, Node? oldPreviousSibling)
        {
            base.NodeIsRemoved(removedNode, oldPreviousSibling);
            UpdateOptions(removedNode, false);
        }

        internal override void SetupElement()
        {
            base.SetupElement();

            NormalizeSelectedness();

            var value = this.GetOwnAttribute(AttributeNames.Value);

            if (value != null)
            {
                UpdateValue(value);
            }
        }

        internal override void Reset()
        {
            foreach (var option in Options.OfType<HtmlOptionElement>())
            {
                option.ResetSelectedness();
            }

            NormalizeSelectedness();
        }

        internal void UpdateMultiple()
        {
            _selectedOption = null;

            if (!IsMultiple)
            {
                var found = false;

                foreach (var option in Options.OfType<HtmlOptionElement>())
                {
                    if (option.IsSelected)
                    {
                        option.SetSelectedness(!found);
                        found = true;
                    }
                }
            }

            NormalizeSelectedness();
        }

        internal void UpdateValue(String value)
        {
            var options = Options;
            IHtmlOptionElement? match = null;

            foreach (var option in options)
            {
                if (match is null && option.Value.Is(value))
                {
                    match = option;
                }

                ((HtmlOptionElement)option).SetSelectedness(false);
            }

            if (match is not null)
            {
                match.IsSelected = true;
            }
        }

        /// <inheritdoc />
        public override Node Clone(Document owner, Boolean deep)
        {
            // Initialize attributes before copying options, so setup cannot normalize
            // the cloned selectedness (including an explicitly cleared selection).
            var node = (HtmlSelectElement)base.Clone(owner, false);
            CloneNode(node, owner, deep);

            if (!node.IsMultiple)
            {
                foreach (var option in node.Options)
                {
                    if (option is HtmlOptionElement selected && selected.IsSelected)
                    {
                        node._selectedOption = selected;
                    }
                }
            }

            return node;
        }

        #endregion

        #region Helpers

        protected override Boolean CanBeValidated()
        {
            return !this.HasDataListAncestor();
        }

        protected override void Check(ValidityState state)
        {
            base.Check(state);
            state.IsValueMissing = IsRequired && String.IsNullOrEmpty(Value);
        }

        #endregion
    }
}
