namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML ordered list (ol) element.
    /// </summary>
    sealed class HtmlOrderedListElement : HtmlElement, IHtmlOrderedListElement
    {
        #region ctor

        public HtmlOrderedListElement(Document owner, String prefix = null)
            : base(owner, TagNames.Ol, prefix, NodeFlags.Special | NodeFlags.HtmlListScoped)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the order is reversed.
        /// </summary>
        public Boolean IsReversed
        {
            get { return this.GetBoolAttribute(AttributeNames.Reversed); }
            set { this.SetBoolAttribute(AttributeNames.Reversed, value); }
        }

        /// <summary>
        /// Gets or sets the start of the numbering.
        /// </summary>
        public Int32 Start
        {
            get { return this.GetOwnAttribute(AttributeNames.Start).ToInteger(1); }
            set { this.SetOwnAttribute(AttributeNames.Start, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets a value within [ 1, a, A, i, I ].
        /// </summary>
        public String Type
        {
            get { return this.GetOwnAttribute(AttributeNames.Type); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        #endregion
    }
}
