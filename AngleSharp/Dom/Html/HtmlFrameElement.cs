namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Extensions;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML frame element.
    /// </summary>
    sealed class HtmlFrameElement : HtmlFrameElementBase
    {
        #region ctor

        public HtmlFrameElement(Document owner, String prefix = null)
            : base(owner, Tags.Frame, prefix, NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the frame cannot be resized.
        /// </summary>
        public Boolean NoResize
        {
            get { return GetOwnAttribute(AttributeNames.NoResize).ToBoolean(false); }
            set { SetOwnAttribute(AttributeNames.NoResize, value.ToString()); }
        }

        #endregion
    }
}
