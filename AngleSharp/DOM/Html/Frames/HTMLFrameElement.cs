using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML frame element.
    /// </summary>
    [DOM("HTMLFrameElement")]
    public sealed class HTMLFrameElement : HTMLFrameElementBase
    {
        #region ctor

        internal HTMLFrameElement()
        {
            _name = Tags.FRAME;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the frame cannot be resized.
        /// </summary>
        [DOM("noResize")]
        public Boolean NoResize
        {
            get { return ToBoolean(GetAttribute(AttributeNames.NORESIZE), false); }
            set { SetAttribute(AttributeNames.NORESIZE, value.ToString()); }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
