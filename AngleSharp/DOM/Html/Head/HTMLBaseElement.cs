using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML base element.
    /// </summary>
    [DOM("HTMLBaseElement")]
    public sealed class HTMLBaseElement : HTMLElement
    {
        #region ctor

        /// <summary>
        /// Creates a HTML base element.
        /// </summary>
        internal HTMLBaseElement()
        {
            _name = Tags.BASE;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the base URI.
        /// </summary>
        [DOM("href")]
        public String Href
        {
            get { return GetAttribute(AttributeNames.HREF); }
            set { SetAttribute(AttributeNames.HREF, value); }
        }

        /// <summary>
        /// Gets or sets the default target frame.
        /// </summary>
        [DOM("target")]
        public String Target
        {
            get { return GetAttribute(AttributeNames.TARGET); }
            set { SetAttribute(AttributeNames.TARGET, value); }
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
