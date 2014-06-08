namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the base class for frame elements.
    /// </summary>
    public abstract class HTMLFrameElementBase : HTMLFrameOwnerElement
    {
        #region ctor

        internal HTMLFrameElementBase()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the frame.
        /// </summary>
        [DomName("name")]
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets or sets the frame source.
        /// </summary>
        [DomName("src")]
        public String Src
        {
            get { return GetAttribute(AttributeNames.Src); }
            set { SetAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets whether or not the frame should have scrollbars.
        /// </summary>
        [DomName("scrolling")]
        public String Scrolling
        {
            get { return GetAttribute(AttributeNames.Scrolling); }
            set { SetAttribute(AttributeNames.Scrolling, value); }
        }

        /// <summary>
        /// Gets the document this frame contains, if there is any and it is available, or null otherwise.
        /// </summary>
        [DomName("contentDocument")]
        public Document ContentDocument
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the URL designating a long description of this image or frame.
        /// </summary>
        [DomName("longDesc")]
        public String LongDesc
        {
            get { return GetAttribute(AttributeNames.LongDesc); }
            set { SetAttribute(AttributeNames.LongDesc, value); }
        }

        /// <summary>
        /// Gets or sets the request frame borders.
        /// </summary>
        [DomName("frameBorder")]
        public String FrameBorder
        {
            get { return GetAttribute(AttributeNames.FrameBorder); }
            set { SetAttribute(AttributeNames.FrameBorder, value); }
        }

        #endregion
    }
}
