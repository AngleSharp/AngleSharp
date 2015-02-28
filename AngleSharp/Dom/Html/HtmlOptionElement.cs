namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML option element.
    /// </summary>
    sealed class HtmlOptionElement : HtmlElement, IHtmlOptionElement
    {
        #region Fields

        Boolean? _selected;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML option element.
        /// </summary>
        public HtmlOptionElement(Document owner)
            : base(owner, Tags.Option, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
        {
        }

        #endregion           
        
        #region Properties

        /// <summary>
        /// Gets or sets if the option is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { return GetAttribute(String.Empty, AttributeNames.Disabled) != null; }
            set { SetAttribute(String.Empty, AttributeNames.Disabled, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        public IHtmlFormElement Form
        {
            get { return GetAssignedForm(); }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public String Label
        {
            get { return GetAttribute(String.Empty, AttributeNames.Label) ?? Text; }
            set { SetAttribute(String.Empty, AttributeNames.Label, value); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public String Value
        {
            get { return GetAttribute(String.Empty, AttributeNames.Value) ?? String.Empty; }
            set { SetAttribute(String.Empty, AttributeNames.Value, value); }
        }

        /// <summary>
        /// Gets the index of the option element.
        /// </summary>
        public Int32 Index
        {
            get
            {
                var group = Parent as HtmlOptionsGroupElement;

                if (group != null)
                {
                    var i = 0;

                    foreach (var child in group.ChildNodes)
                    {
                        if (Object.ReferenceEquals(child, this))
                            return i;

                        i++;
                    }
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
        public Boolean IsDefaultSelected
        {
            get { return GetAttribute(String.Empty, AttributeNames.Selected) != null; }
            set { SetAttribute(String.Empty, AttributeNames.Selected, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the option is currently selected.
        /// </summary>
        public Boolean IsSelected
        {
            get { return _selected.HasValue ? _selected.Value : IsDefaultSelected; }
            set { _selected = value; }
        }

        #endregion
    }
}
