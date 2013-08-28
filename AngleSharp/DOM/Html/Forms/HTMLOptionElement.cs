using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML option element.
    /// </summary>
    [DOM("HTMLOptionElement")]
    public sealed class HTMLOptionElement : HTMLElement, ISelectScopeElement, IImplClosed
    {
        #region Members

        Boolean? _selected;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML option element.
        /// </summary>
        internal HTMLOptionElement()
        {
            _name = Tags.OPTION;
        }

        #endregion           
        
        #region Properties

        /// <summary>
        /// Gets or sets if the option is enabled or disabled.
        /// </summary>
        [DOM("disabled")]
        public Boolean Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        [DOM("form")]
        public HTMLFormElement Form
        {
            get { return GetAssignedForm(); }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [DOM("label")]
        public String Label
        {
            get { return GetAttribute("label"); }
            set { SetAttribute("label", value); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [DOM("value")]
        public String Value
        {
            get { return GetAttribute("value") ?? String.Empty; }
            set { SetAttribute("value", value); }
        }

        /// <summary>
        /// Gets the index of the option element.
        /// </summary>
        [DOM("index")]
        public Int32 Index
        {
            get
            {
                var group = _parent as HTMLOptGroupElement;

                if(group != null)
                {
                    int i = 0;

                    foreach (var child in group.Children)
                    {
                        if (child == this)
                            return i;
                        else
                            i++;
                    }
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the text of the option.
        /// </summary>
        [DOM("text")]
        public String Text
        {
            get { return TextContent.CollapseAndStrip(); }
            set { TextContent = value; }
        }

        /// <summary>
        /// Gets or sets if the option is selected by default.
        /// </summary>
        [DOM("defaultSelected")]
        public Boolean DefaultSelected
        {
            get { return GetAttribute("selected") != null; }
            set { SetAttribute("selected", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the option is currently selected.
        /// </summary>
        [DOM("selected")]
        public Boolean Selected
        {
            get { return _selected.HasValue ? _selected.Value : DefaultSelected; }
            set { _selected = value; }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}
