using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML iframe element.
    /// </summary>
    [DOM("HTMLIFrameElement")]
    public sealed class HTMLIFrameElement : HTMLFrameElementBase
    {
        #region ctor

        internal HTMLIFrameElement()
        {
            _name = Tags.IFRAME;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        [DOM("align")]
        public Alignment Align
        {
            get { return ToEnum(GetAttribute(AttributeNames.ALIGN), Alignment.Bottom); }
            set { SetAttribute(AttributeNames.ALIGN, value.ToString()); }
        }

        #endregion

        #region Internal properties

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
