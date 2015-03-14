namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the select element.
    /// </summary>
    sealed class HtmlSelectElement : HtmlFormControlElementWithState, IHtmlSelectElement
    {
        #region Fields

        OptionsCollection _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML select element.
        /// </summary>
        public HtmlSelectElement(Document owner)
            : base(owner, Tags.Select)
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
            get
            {
                var options = Options;
                var result = new List<IHtmlOptionElement>();

                for (int i = 0; i < options.Length; i++)
                {
                    var option = options.GetOptionAt(i);

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

                for (int i = 0; i < options.Length; i++)
                {
                    var option = options.GetOptionAt(i);

                    if (option.IsSelected)
                        return option.Value;
                }

                return null;
            }
            set
            {
                var options = Options;

                for (int i = 0; i < options.Length; i++)
                {
                    var option = options.GetOptionAt(i);
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

            for (int i = 0; i < options.Length; i++)
            {
                var option = options.GetOptionAt(i);
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
