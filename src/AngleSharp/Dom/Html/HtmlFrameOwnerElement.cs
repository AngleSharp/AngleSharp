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

        public Boolean CanContainRangeEndpoint
        {
            get;
            private set;
        }

        public Int32 DisplayWidth
        {
            get { return this.GetOwnAttribute(AttributeNames.Width).ToInteger(0); }
            set { this.SetOwnAttribute(AttributeNames.Width, value.ToString()); }
        }

        public Int32 DisplayHeight
        {
            get { return this.GetOwnAttribute(AttributeNames.Height).ToInteger(0); }
            set { this.SetOwnAttribute(AttributeNames.Height, value.ToString()); }
        }

        public Int32 MarginWidth
        {
            get { return this.GetOwnAttribute(AttributeNames.MarginWidth).ToInteger(0); }
            set { this.SetOwnAttribute(AttributeNames.MarginWidth, value.ToString()); }
        }

        public Int32 MarginHeight
        {
            get { return this.GetOwnAttribute(AttributeNames.MarginHeight).ToInteger(0); }
            set { this.SetOwnAttribute(AttributeNames.MarginHeight, value.ToString()); }
        }

        #endregion
    }
}
