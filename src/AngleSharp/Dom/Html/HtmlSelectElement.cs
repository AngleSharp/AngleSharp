namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the select element.
    /// </summary>
    sealed class HtmlSelectElement : HtmlFormControlElementWithState, IHtmlSelectElement
    {
        #region Fields

        private OptionsCollection _options;
        private HtmlCollection<IHtmlOptionElement> _selected;

        #endregion

        #region ctor
        
        public HtmlSelectElement(Document owner, String prefix = null)
            : base(owner, TagNames.Select, prefix)
        {
        }

        #endregion

        #region Index

        public IHtmlOptionElement this[Int32 index]
        {
            get { return Options.GetOptionAt(index); }
            set { Options.SetOptionAt(index, value); }
        }

        #endregion

        #region Properties

        public Int32 Size
        {
            get { return this.GetOwnAttribute(AttributeNames.Size).ToInteger(0); }
            set { this.SetOwnAttribute(AttributeNames.Size, value.ToString()); }
        }

        public Boolean IsRequired
        {
            get { return this.GetBoolAttribute(AttributeNames.Required); }
            set { this.SetBoolAttribute(AttributeNames.Required, value); }
        }

        public IHtmlCollection<IHtmlOptionElement> SelectedOptions
        {
            get { return _selected ?? (_selected = new HtmlCollection<IHtmlOptionElement>(Options.Where(m => m.IsSelected))); }
        }

        public Int32 SelectedIndex
        {
            get { return Options.SelectedIndex; }
        }

        public String Value
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
            set
            {
                UpdateValue(value);
            }
        }

        public Int32 Length
        {
            get { return Options.Length; }
        }

        public Boolean IsMultiple
        {
            get { return this.GetBoolAttribute(AttributeNames.Multiple); }
            set { this.SetBoolAttribute(AttributeNames.Multiple, value); }
        }

        public IHtmlOptionsCollection Options
        {
            get { return _options ?? (_options = new OptionsCollection(this)); }
        }

        public String Type
        {
            get { return IsMultiple ? InputTypeNames.SelectMultiple : InputTypeNames.SelectOne; }
        }

        #endregion

        #region Methods

        public void AddOption(IHtmlOptionElement element, IHtmlElement before = null)
        {
            Options.Add(element, before);
        }

        public void AddOption(IHtmlOptionsGroupElement element, IHtmlElement before = null)
        {
            Options.Add(element, before);
        }

        public void RemoveOptionAt(Int32 index)
        {
            Options.Remove(index);
        }

        #endregion

        #region Internal Methods

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

        internal override void ConstructDataSet(FormDataSet dataSet, IHtmlElement submitter)
        {
            var options = Options;

            for (var i = 0; i < options.Length; i++)
            {
                var option = options.GetOptionAt(i);

                if (option.IsSelected && !option.IsDisabled)
                {
                    dataSet.Append(Name, option.Value, Type);
                }
            }
        }

        internal override void SetupElement()
        {
            base.SetupElement();

            var value = this.GetOwnAttribute(AttributeNames.Value);

            if (value != null)
            {
                UpdateValue(value);
            }
        }

        internal override void Reset()
        {
            var options = Options;
            var selected = 0;
            var maxSelected = 0;

            for (var i = 0; i < options.Length; i++)
            {
                var option = options.GetOptionAt(i);
                option.IsSelected = option.IsDefaultSelected;

                if (option.IsSelected)
                {
                    maxSelected = i;
                    selected++;
                }
            }

            if (selected != 1 && !IsMultiple && options.Length > 0)
            {
                foreach (var option in options)
                {
                    option.IsSelected = false;
                }

                options[maxSelected].IsSelected = true;
            }
        }

        internal void UpdateValue(String value)
        {
            var options = Options;

            foreach (var option in options)
            {
                var selected = option.Value.Isi(value);
                option.IsSelected = selected;
            }
        }

        #endregion

        #region Helpers

        protected override Boolean CanBeValidated()
        {
            return !this.HasDataListAncestor();
        }

        protected override void Check(ValidityState state)
        {
            var value = Value;
            state.IsValueMissing = IsRequired && String.IsNullOrEmpty(value);
        }

        #endregion
    }
}
