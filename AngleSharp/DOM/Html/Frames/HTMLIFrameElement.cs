using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML iframe element.
    /// </summary>
    public sealed class HTMLIFrameElement : HTMLFrameElementBase
    {
        #region Constant

        /// <summary>
        /// The iframe tag.
        /// </summary>
        internal const String Tag = "iframe";

        #endregion

        #region ctor

        internal HTMLIFrameElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        [DOM("align")]
        public Alignment Align
        {
            get { return ToEnum(GetAttribute("align"), Alignment.Bottom); }
            set { SetAttribute("align", value.ToString()); }
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
