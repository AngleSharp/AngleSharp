namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Extensions;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the base class for frame owned elements.
    /// </summary>
    abstract class HtmlFrameOwnerElement : HtmlElement
    {
        #region ctor

        public HtmlFrameOwnerElement(Document owner, String name, String prefix, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, flags)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the status if the element can contain a range endpoint.
        /// </summary>
        public Boolean CanContainRangeEndpoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the width of the frame.
        /// </summary>
        public Int32 DisplayWidth
        {
            get { return GetOwnAttribute(AttributeNames.Width).ToInteger(0); }
            set { SetOwnAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets the height of the frame.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetOwnAttribute(AttributeNames.Height).ToInteger(0); }
            set { SetOwnAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the frame margin width, in pixels.
        /// </summary>
        public Int32 MarginWidth
        {
            get { return GetOwnAttribute(AttributeNames.MarginWidth).ToInteger(0); }
            set { SetOwnAttribute(AttributeNames.MarginWidth, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the frame margin height, in pixels.
        /// </summary>
        public Int32 MarginHeight
        {
            get { return GetOwnAttribute(AttributeNames.MarginHeight).ToInteger(0); }
            set { SetOwnAttribute(AttributeNames.MarginHeight, value.ToString()); }
        }

        #endregion
    }
}
