namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML optgroup element.
    /// </summary>
    sealed class HtmlOptionsGroupElement : HtmlElement, IHtmlOptionsGroupElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML optgroup element.
        /// </summary>
        public HtmlOptionsGroupElement(Document owner, String prefix = null)
            : base(owner, Tags.Optgroup, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public String Label
        {
            get { return GetOwnAttribute(AttributeNames.Label); }
            set { SetOwnAttribute(AttributeNames.Label, value); }
        }

        /// <summary>
        /// Gets or sets if the optgroup is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { return GetOwnAttribute(AttributeNames.Disabled) != null; }
            set { SetOwnAttribute(AttributeNames.Disabled, value ? String.Empty : null); }
        }

        #endregion
    }
}
