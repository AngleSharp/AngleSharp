namespace AngleSharp.DOM.Html
{
    using System;

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
            get { return ToBoolean(GetAttribute(AttributeNames.NoResize), false); }
            set { SetAttribute(AttributeNames.NoResize, value.ToString()); }
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
