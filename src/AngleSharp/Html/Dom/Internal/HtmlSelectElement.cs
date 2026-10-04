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

                return String.Empty;
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
            var options = Options;
            foreach (var option in options)
            {
                if (option.IsSelected && !IsOptionDisabled(option))
                {
                    dataSet.Append(Name!, option.Value, Type);
                }
            }
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
            foreach (var option in Options)
            {
                SetSelectedness(option, option.IsDefaultSelected, resetDirtiness: true);
            }
            NormalizeSelectedness();
        }

        // https://html.spec.whatwg.org/multipage/form-elements.html#selectedness-setting-algorithm
        internal void NormalizeSelectedness(IHtmlOptionElement? newlySelected = null)
        {
            if (IsMultiple)
            {
                return;
            }
            IHtmlOptionElement? firstEnabled = null;
            IHtmlOptionElement? lastSelected = null;
            foreach (var option in Options)
            {
                if (firstEnabled is null && !IsOptionDisabled(option))
                {
                    firstEnabled = option;
                }
                if (option.IsSelected)
                {
                    if (newlySelected is not null && !Object.ReferenceEquals(option, newlySelected))
                    {
                        SetSelectedness(option, false);
                        continue;
                    }
                    if (lastSelected is not null)
                    {
                        SetSelectedness(lastSelected, false);
                    }
                    lastSelected = option;
                }
            }
            if (lastSelected is null && Size <= 1 && firstEnabled is not null)
            {
                SetSelectedness(firstEnabled, true);
            }
        }

        internal void UpdateValue(String value)
        {
            IHtmlOptionElement? matching = null;
            foreach (var option in Options)
            {
                if (matching is null && option.Value.Is(value))
                {
                    matching = option;
                }
                SetSelectedness(option, false);
            }
            if (matching is not null)
            {
                matching.IsSelected = true;
            }
        }

        internal static void SetSelectedness(IHtmlOptionElement option, Boolean value, Boolean resetDirtiness = false)
        {
            if (option is HtmlOptionElement element)
            {
                element.SetSelectedness(value, resetDirtiness);
            }
            else
            {
                option.IsSelected = value;
            }
        }

        internal static HtmlSelectElement? GetSelect(IElement element)
        {
            for (var parent = element.ParentElement; parent is not null; parent = parent.ParentElement)
            {
                if (parent is HtmlSelectElement select)
                {
                    return select;
                }
            }
            return null;
        }

        internal void NormalizeInsertedOptions(Node node)
        {
            if (!ContainsOptions(node))
            {
                return;
            }
            // Fragment insertion attaches every child before running their insertion steps.
            // Choose the last selected option entering this select, including later children
            // whose insertion steps have not run yet, before clearing other selectedness.
            var selected = Options.OfType<HtmlOptionElement>().LastOrDefault(option =>
                option.IsSelected && !Object.ReferenceEquals(option.CachedSelect, this));
            NormalizeSelectedness(selected);
            CacheOptionOwners(node, this);
        }

        private static void CacheOptionOwners(Node node, HtmlSelectElement? select)
        {
            if (node is HtmlOptionElement option)
            {
                option.CachedSelect = select;
            }
            else
            {
                foreach (var descendant in node.Descendants<HtmlOptionElement>())
                {
                    descendant.CachedSelect = select;
                }
            }
        }

        internal void NormalizeRemovedOptions(Node node)
        {
            if (!ContainsOptions(node))
            {
                return;
            }
            CacheOptionOwners(node, null);
            NormalizeSelectedness();
        }

        private static Boolean ContainsOptions(Node node) =>
            node is HtmlOptionElement || node.Descendants<HtmlOptionElement>().Any();

        private static Boolean IsOptionDisabled(IHtmlOptionElement option) =>
            option.IsDisabled || option.ParentElement is IHtmlOptionsGroupElement group && group.IsDisabled;

        #endregion

        #region Helpers

        protected override void NodeIsInserted(Node newNode)
        {
            base.NodeIsInserted(newNode);
            NormalizeInsertedOptions(newNode);
        }

        protected override void NodeIsRemoved(Node removedNode, Node? oldPreviousSibling)
        {
            base.NodeIsRemoved(removedNode, oldPreviousSibling);
            NormalizeRemovedOptions(removedNode);
        }

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
