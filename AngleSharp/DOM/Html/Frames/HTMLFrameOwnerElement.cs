namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the base class for frame owned elements.
    /// </summary>
    public abstract class HTMLFrameOwnerElement : HTMLElement
    {
        #region ctor

        internal HTMLFrameOwnerElement()
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
        [DomName("width")]
        public Int32 Width
        {
            get { return ToInteger(GetAttribute(AttributeNames.Width), 0); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets the height of the frame.
        /// </summary>
        [DomName("height")]
        public Int32 Height
        {
            get { return ToInteger(GetAttribute(AttributeNames.Height), 0); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the frame margin width, in pixels.
        /// </summary>
        [DomName("marginWidth")]
        public Int32 MarginWidth
        {
            get { return ToInteger(GetAttribute(AttributeNames.MarginWidth), 0); }
            set { SetAttribute(AttributeNames.MarginWidth, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the frame margin height, in pixels.
        /// </summary>
        [DomName("marginHeight")]
        public Int32 MarginHeight
        {
            get { return ToInteger(GetAttribute(AttributeNames.MarginHeight), 0); }
            set { SetAttribute(AttributeNames.MarginHeight, value.ToString()); }
        }

        #endregion
    }
}
