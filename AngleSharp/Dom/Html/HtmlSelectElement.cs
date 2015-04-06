namespace AngleSharp.Dom.Html
{
    using System;
    using System.Linq;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the select element.
    /// </summary>
    sealed class HtmlSelectElement : HtmlFormControlElementWithState, IHtmlSelectElement
    {
        #region Fields

        OptionsCollection _options;
        HtmlCollection<IHtmlOptionElement> _selected;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML select element.
        /// </summary>
        public HtmlSelectElement(Document owner, String prefix = null)
            : base(owner, Tags.Select, prefix)
        {
            RegisterAttributeObserver(AttributeNames.Value, value => Value = value);
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets or sets an option element.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The option at the given index.</returns>
        public IHtmlOptionElement this[Int32 index]
        {
            get { return Options.GetOptionAt(index); }
            set { Options.SetOptionAt(index, value); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the size of the element.
        /// </summary>
        public Int32 Size
        {
            get { return GetOwnAttribute(AttributeNames.Size).ToInteger(0); }
            set { SetOwnAttribute(AttributeNames.Size, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets if the field is required.
        /// </summary>
        public Boolean IsRequired
        {
            get { return GetOwnAttribute(AttributeNames.Required) != null; }
            set { SetOwnAttribute(AttributeNames.Required, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the set of options that are selected.
        /// </summary>
        public IHtmlCollection<IHtmlOptionElement> SelectedOptions
        {
            get { return _selected ?? (_selected = new HtmlCollection<IHtmlOptionElement>(Options.Where(m => m.IsSelected))); }
        }

        /// <summary>
        /// Gets the index of the first selected option element.
        /// </summary>
        public Int32 SelectedIndex
        {
            get { return Options.SelectedIndex; }
        }

        /// <summary>
        /// Gets or sets the value of this form control, that is, of the first selected option.
        /// </summary>
        public String Value
        {
            get
            {
                var options = Options;

                foreach (var option in options)
                {
                    if (option.IsSelected)
                        return option.Value;
                }

                return null;
            }
            set
            {
                var options = Options;

                foreach (var option in options)
                {
                    option.IsSelected = option.Value == value;
                }
            }
        }

        /// <summary>
        /// Gets the number of option elements in this select element.
        /// </summary>
        public Int32 Length
        {
            get { return Options.Length; }
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
        /// Gets the set of option elements contained by this element. 
        /// </summary>
        public IHtmlOptionsCollection Options
        {
            get { return _options ?? (_options = new OptionsCollection(this)); }
        }

        /// <summary>
        /// Gets the form control's type.
        /// </summary>
        public String Type
        {
            get { return IsMultiple ? InputTypeNames.SelectMultiple : InputTypeNames.SelectOne; }
        }

        #endregion

        #region Methods

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
        /// Adds the element to the options collection.
        /// </summary>
        /// <param name="element">The option element to add.</param>
        /// <param name="before">The following element.</param>
        public void AddOption(IHtmlOptionElement element, IHtmlElement before = null)
        {
            Options.Add(element, before);
        }

        /// <summary>
        /// Adds the element to the options collection.
        /// </summary>
        /// <param name="element">The group element to add.</param>
        /// <param name="before">The following element.</param>
        public void AddOption(IHtmlOptionsGroupElement element, IHtmlElement before = null)
        {
            Options.Add(element, before);
        }

        /// <summary>
        /// Removes the option with the given index from the collection.
        /// </summary>
        /// <param name="index">The index of the element to remove.</param>
        public void RemoveOptionAt(Int32 index)
        {
            Options.Remove(index);
        }

        #endregion

        #region Helpers

        internal override void ConstructDataSet(FormDataSet dataSet, HtmlElement submitter)
        {
            var options = Options;

            for (int i = 0; i < options.Length; i++)
            {
                var option = options.GetOptionAt(i);

                if (option.IsSelected && !option.IsDisabled)
                    dataSet.Append(Name, option.Value, Type);
            }
        }

        protected override Boolean CanBeValidated()
        {
            return this.HasDataListAncestor() == false;
        }

        /// <summary>
        /// Resets the form control to its initial value.
        /// </summary>
        internal override void Reset()
        {
            var options = Options;
            var selected = 0;
            var maxSelected = 0;

            for (int i = 0; i < options.Length; i++)
            {
                var option = options.GetOptionAt(i);
                option.IsSelected = option.IsDefaultSelected;

                if (option.IsSelected)
                {
                    maxSelected = i;
                    selected++;
                }
            }

            if (selected != 1 && IsMultiple == false && options.Length > 0)
            {
                foreach (var option in options)
                    option.IsSelected = false;

                options[maxSelected].IsSelected = true;
            }
        }

        /// <summary>
        /// Checks the form control for validity.
        /// </summary>
        /// <param name="state">The element's validity state tracker.</param>
        protected override void Check(ValidityState state)
        {
            var value = Value;
            state.IsValueMissing = IsRequired && String.IsNullOrEmpty(value);
        }

        #endregion
    }
}
