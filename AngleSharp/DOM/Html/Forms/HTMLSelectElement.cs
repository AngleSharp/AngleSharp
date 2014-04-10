using AngleSharp.DOM.Collections;
using System;
using System.Collections.Generic;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the select element.
    /// </summary>
    [DOM("HTMLSelectElement")]
    public sealed class HTMLSelectElement : HTMLFormControlElementWithState
    {
        #region Members

        HTMLLiveCollection<HTMLOptionElement> _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML select element.
        /// </summary>
        internal HTMLSelectElement()
        {
            _name = Tags.Select;
            _options = new HTMLLiveCollection<HTMLOptionElement>(this);
            WillValidate = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the field is required.
        /// </summary>
        [DOM("required")]
        public Boolean Required
        {
            get { return GetAttribute("required") != null; }
            set { SetAttribute("required", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the set of options that are selected.
        /// </summary>
        [DOM("selectedOptions")]
        public HTMLCollection SelectedOptions
        {
            get 
            {
                var result = new List<Element>();

                foreach (var option in _options.Elements)
                    if (option.Selected)
                        result.Add(option);

                return new HTMLStaticCollection(result);
            }
        }

        /// <summary>
        /// Gets the index of the first selected option element.
        /// </summary>
        [DOM("selectedIndex")]
        public Int32 SelectedIndex
        {
            get 
            { 
                var index = 0;

                foreach (var option in _options.Elements)
                {
                    if (option.Selected)
                        return index;

                    index++;
                }

                return -1;
            }
        }

        /// <summary>
        /// Gets or sets the value of this form control, that is, of the first selected option.
        /// </summary>
        [DOM("value")]
        public String Value
        {
            get
            {
                foreach (var option in _options.Elements)
                {
                    if (option.Selected)
                        return option.Value;
                }

                return null;
            }
            set
            {
                foreach (var option in _options.Elements)
                    option.Selected = option.Value == value;
            }
        }

        /// <summary>
        /// Gets the number of option elements in this select element.
        /// </summary>
        [DOM("length")]
        public Int32 Length
        {
            get { return Options.Length; }
        }

        /// <summary>
        /// Gets the multiple HTML attribute, whichindicates whether multiple items can be selected.
        /// </summary>
        [DOM("multiple")]
        public Boolean Multiple
        {
            get { return GetAttribute("multiple") != null; }
            set { SetAttribute("multiple", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the set of option elements contained by this element. 
        /// </summary>
        [DOM("options")]
        public HTMLCollection Options
        {
            get { return _options; }
        }

        /// <summary>
        /// Gets the form control's type.
        /// </summary>
        [DOM("type")]
        public SelectType Type
        {
            get { return Multiple ? SelectType.SelectMultiple : SelectType.SelectOne; }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
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
            var options = _options.Elements;

            foreach (var option in options)
            {
                if (option.Selected && !option.Disabled)
                    dataSet.Append(Name, option.Value, Multiple ? "select-one" : "select-multiple");
            }
        }

        /// <summary>
        /// Entry point for attributes to notify about a change (modified, added, removed).
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        internal override void OnAttributeChanged(String name)
        {
            if (name.Equals("value", StringComparison.Ordinal))
                Value = _attributes["value"].Value;
            else
                base.OnAttributeChanged(name);
        }

        /// <summary>
        /// Resets the form control to its initial value.
        /// </summary>
        internal override void Reset()
        {
            foreach (var option in _options.Elements)
                option.Selected = option.DefaultSelected;
        }

        /// <summary>
        /// Checks the form control for validity.
        /// </summary>
        /// <param name="state">The element's validity state tracker.</param>
        protected override void Check(ValidityState state)
        {
            //TODO
        }

        #endregion
    }
}
