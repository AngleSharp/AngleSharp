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

        public HtmlFrameElementBase(Document owner, String name, String prefix, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, flags | NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the frame.
        /// </summary>
        public String Name
        {
            get { return GetOwnAttribute(AttributeNames.Name); }
            set { SetOwnAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets or sets the frame source.
        /// </summary>
        public String Source
        {
            get { return GetUrlAttribute(AttributeNames.Src); }
            set { SetOwnAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets whether or not the frame should have scrollbars.
        /// </summary>
        public String Scrolling
        {
            get { return GetOwnAttribute(AttributeNames.Scrolling); }
            set { SetOwnAttribute(AttributeNames.Scrolling, value); }
        }

        /// <summary>
        /// Gets the document this frame contains, if there is any and it is
        /// available, or null otherwise.
        /// </summary>
        public IDocument ContentDocument
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the URL designating a long description of this image
        /// or frame.
        /// </summary>
        public String LongDesc
        {
            get { return GetOwnAttribute(AttributeNames.LongDesc); }
            set { SetOwnAttribute(AttributeNames.LongDesc, value); }
        }

        /// <summary>
        /// Gets or sets the request frame borders.
        /// </summary>
        public String FrameBorder
        {
            get { return GetOwnAttribute(AttributeNames.FrameBorder); }
            set { SetOwnAttribute(AttributeNames.FrameBorder, value); }
        }

        #endregion
    }
}
