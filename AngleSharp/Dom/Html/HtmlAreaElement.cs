namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the area element.
    /// </summary>
    sealed class HtmlAreaElement : HtmlUrlBaseElement, IHtmlAreaElement
    {
        #region ctor

        /// <summary>
        /// Creates a new area element.
        /// </summary>
        public HtmlAreaElement(Document owner, String prefix = null)
            : base(owner, TagNames.Area, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the alternative text for the element.
        /// </summary>
        public String AlternativeText
        {
            get { return this.GetOwnAttribute(AttributeNames.Alt); }
            set { this.SetOwnAttribute(AttributeNames.Alt, value); }
        }

        /// <summary>
        /// Gets or sets the coordinates to define the hot-spot region.
        /// </summary>
        public String Coordinates
        {
            get { return this.GetOwnAttribute(AttributeNames.Coords); }
            set { this.SetOwnAttribute(AttributeNames.Coords, value); }
        }

        /// <summary>
        /// Gets or sets the shape of the hot-spot, limited to known values.
        /// The known values are: circle, default. poly, rect. The missing
        /// value is rect.
        /// </summary>
        public String Shape
        {
            get { return this.GetOwnAttribute(AttributeNames.Shape); }
            set { this.SetOwnAttribute(AttributeNames.Shape, value); }
        }

        #endregion
    }
}
