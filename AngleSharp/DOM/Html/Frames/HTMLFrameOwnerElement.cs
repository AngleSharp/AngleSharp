using System;

namespace AngleSharp.DOM.Html
{
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
        /// Gets or sets the location of the frame.
        /// </summary>
        public String Location
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the width of the frame.
        /// </summary>
        public Int32 Width
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the height of the frame.
        /// </summary>
        public Int32 Height
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the width of the margin of the frame.
        /// </summary>
        public Int32 MarginWidth
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the height of the margin of the frame.
        /// </summary>
        public Int32 MarginHeight
        {
            get;
            private set;
        }

        #endregion
    }
}
