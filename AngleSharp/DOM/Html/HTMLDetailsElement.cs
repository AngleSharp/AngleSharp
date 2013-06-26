using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML details element.
    /// </summary>
    [DOM("HTMLDetailsElement")]
    public sealed class HTMLDetailsElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The details tag.
        /// </summary>
        internal const String Tag = "details";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML details element.
        /// </summary>
        internal HTMLDetailsElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the details element is open.
        /// </summary>
        [DOM("open")]
        public Boolean Open
        {
            get { return GetAttribute("open") != null; }
            set { SetAttribute("open", value ? string.Empty : null); }
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
