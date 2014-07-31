namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the base class for frame owned elements.
    /// </summary>
    abstract class HTMLFrameOwnerElement : HTMLElement
    {
        #region ctor

        internal HTMLFrameOwnerElement(String name, NodeFlags flags = NodeFlags.None)
            : base(name, flags)
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
            get { return GetAttribute(AttributeNames.Width).ToInteger(0); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets the height of the frame.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetAttribute(AttributeNames.Height).ToInteger(0); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the frame margin width, in pixels.
        /// </summary>
        public Int32 MarginWidth
        {
            get { return GetAttribute(AttributeNames.MarginWidth).ToInteger(0); }
            set { SetAttribute(AttributeNames.MarginWidth, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the frame margin height, in pixels.
        /// </summary>
        public Int32 MarginHeight
        {
            get { return GetAttribute(AttributeNames.MarginHeight).ToInteger(0); }
            set { SetAttribute(AttributeNames.MarginHeight, value.ToString()); }
        }

        #endregion
    }
}
