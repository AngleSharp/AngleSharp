namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the select element.
    /// </summary>
    sealed class HTMLSelectElement : HTMLFormControlElementWithState, IHtmlSelectElement
    {
        #region Fields

        readonly OptionsCollection _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML select element.
        /// </summary>
        public HTMLSelectElement()
            : base(Tags.Select)
        {
            _options = new OptionsCollection(this);
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
            get { return _options.GetOptionAt(index); }
            set { _options.SetOptionAt(index, value); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the size of the element.
        /// </summary>
        public Int32 Size
        {
            get { return GetAttribute(AttributeNames.Size).ToInteger(0); }
            set { SetAttribute(AttributeNames.Size, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets if the field is required.
        /// </summary>
        public Boolean IsRequired
        {
            get { return GetAttribute(AttributeNames.Required) != null; }
            set { SetAttribute(AttributeNames.Required, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the set of options that are selected.
        /// </summary>
        public IHtmlCollection SelectedOptions
        {
            get 
            {
                var result = new List<IHtmlOptionElement>();

                for (int i = 0; i < _options.Length; i++)
                {
                    var option = _options.GetOptionAt(i);

                    if (option.IsSelected)
                        result.Add(option);
                }

                return new HtmlCollection<IHtmlOptionElement>(result);
            }
        }

        /// <summary>
        /// Gets the index of the first selected option element.
        /// </summary>
        public Int32 SelectedIndex
        {
            get { return _options.SelectedIndex; }
        }

        /// <summary>
        /// Gets or sets the value of this form control, that is, of the first selected option.
        /// </summary>
        public String Value
        {
            get
            {
                for (int i = 0; i < _options.Length; i++)
                {
                    var option = _options.GetOptionAt(i);

                    if (option.IsSelected)
                        return option.Value;
                }

                return null;
            }
            set
            {
                for (int i = 0; i < _options.Length; i++)
                {
                    var option = _options.GetOptionAt(i);
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
            get { return GetAttribute(AttributeNames.Multiple) != null; }
            set { SetAttribute(AttributeNames.Multiple, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the set of option elements contained by this element. 
        /// </summary>
        public IHtmlOptionsCollection Options
        {
            get { return _options; }
        }

        /// <summary>
        /// Gets the form control's type.
        /// </summary>
        public String Type
        {
            get { return IsMultiple ? "select-multiple" : "select-one"; }
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
            _options.Add(element, before);
        }

        /// <summary>
        /// Adds the element to the options collection.
        /// </summary>
        /// <param name="element">The group element to add.</param>
        /// <param name="before">The following element.</param>
        public void AddOption(IHtmlOptionsGroupElement element, IHtmlElement before = null)
        {
            _options.Add(element, before);
        }

        /// <summary>
        /// Removes the option with the given index from the collection.
        /// </summary>
        /// <param name="index">The index of the element to remove.</param>
        public void RemoveOptionAt(Int32 index)
        {
            _options.Remove(index);
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// An enumeration with possible select types.
        /// </summary>
        public enum SelectType : ushort
        {
            /// <summary>
            /// Only one element can be selected.
            /// </summary>
            SelectOne,
            /// <summary>
            /// Multiple elements can be selected.
            /// </summary>
            SelectMultiple
        }

        #endregion

        #region Helpers

        internal override void ConstructDataSet(FormDataSet dataSet, HTMLElement submitter)
        {
            for (int i = 0; i < _options.Length; i++)
            {
                var option = _options.GetOptionAt(i);

                if (option.IsSelected && !option.IsDisabled)
                    dataSet.Append(Name, option.Value, Type);
            }
        }

        internal override void Close()
        {
            base.Close();
            RegisterAttributeHandler(AttributeNames.Value, value => Value = value);
            Value = GetAttribute(AttributeNames.Value);
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
            for (int i = 0; i < _options.Length; i++)
            {
                var option = _options.GetOptionAt(i);
                option.IsSelected = option.IsDefaultSelected;
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
