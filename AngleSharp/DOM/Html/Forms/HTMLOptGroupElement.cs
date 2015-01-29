namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML optgroup element.
    /// </summary>
    sealed class HTMLOptGroupElement : HTMLElement, IHtmlOptionsGroupElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML optgroup element.
        /// </summary>
        public HTMLOptGroupElement(Document owner)
            : base(owner, Tags.Optgroup, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public String Label
        {
            get { return GetAttribute(AttributeNames.Label); }
            set { SetAttribute(AttributeNames.Label, value); }
        }

        /// <summary>
        /// Gets or sets if the optgroup is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { return GetAttribute(AttributeNames.Disabled) != null; }
            set { SetAttribute(AttributeNames.Disabled, value ? String.Empty : null); }
        }

        #endregion
    }
}
