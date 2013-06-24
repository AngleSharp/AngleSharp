using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML option element.
    /// </summary>
    public sealed class HTMLOptionElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The option tag.
        /// </summary>
        internal const string Tag = "option";

        #endregion

        #region Members

        bool selectedness;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML option element.
        /// </summary>
        internal HTMLOptionElement()
        {
            _name = Tag;
            Text = string.Empty;
            Value = string.Empty;
            Selected = false;
            DefaultSelected = false;
        }

        #endregion           
        
        #region Properties

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public String Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Gets or sets if the option is enabled or disabled.
        /// </summary>
        public Boolean Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        public HTMLFormElement Form
        {
            get 
            {
                if (_parent != null && _parent is HTMLSelectElement)
                    return ((HTMLSelectElement)_parent).Form;

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public String Label
        {
            get { return GetAttribute("label"); }
            set { SetAttribute("label", value); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public String Value
        {
            get { return GetAttribute("value"); }
            set { SetAttribute("value", value); }
        }

        /// <summary>
        /// Gets the index of the option element.
        /// </summary>
        public Int32 Index
        {
            get
            {
                var group = _parent as HTMLOptGroupElement;

                if(group != null)
                {
                    for (int i = 0; i < group.ChildElementCount; i++)
			            if(group.Children[i] == this)
                            return i;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the text of the option.
        /// </summary>
        public String Text
        {
            get { return TextContent.CollapseAndStrip(); }
            set { TextContent = value; }
        }

        /// <summary>
        /// Gets or sets if the option is selected by default.
        /// </summary>
        public Boolean DefaultSelected
        {
            get { return GetAttribute("selected") != null; }
            set { SetAttribute("selected", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the option is currently selected.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#ask-for-a-reset
        /// </summary>
        public Boolean Selected
        {
            get { return selectedness; }
            set { selectedness = value; }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}
