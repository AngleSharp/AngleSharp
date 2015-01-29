namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the base class for frame elements.
    /// </summary>
    abstract class HtmlFrameElementBase : HtmlFrameOwnerElement
    {
        #region ctor

        public HtmlFrameElementBase(Document owner, String name, NodeFlags flags = NodeFlags.None)
            : base(owner, name, flags | NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the frame.
        /// </summary>
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets or sets the frame source.
        /// </summary>
        public String Source
        {
            get { return GetAttribute(AttributeNames.Src); }
            set { SetAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets whether or not the frame should have scrollbars.
        /// </summary>
        public String Scrolling
        {
            get { return GetAttribute(AttributeNames.Scrolling); }
            set { SetAttribute(AttributeNames.Scrolling, value); }
        }

        /// <summary>
        /// Gets the document this frame contains, if there is any and it is available, or null otherwise.
        /// </summary>
        public IDocument ContentDocument
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the URL designating a long description of this image or frame.
        /// </summary>
        public String LongDesc
        {
            get { return GetAttribute(AttributeNames.LongDesc); }
            set { SetAttribute(AttributeNames.LongDesc, value); }
        }

        /// <summary>
        /// Gets or sets the request frame borders.
        /// </summary>
        public String FrameBorder
        {
            get { return GetAttribute(AttributeNames.FrameBorder); }
            set { SetAttribute(AttributeNames.FrameBorder, value); }
        }

        #endregion
    }
}
