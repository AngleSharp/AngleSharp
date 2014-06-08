namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML details element.
    /// </summary>
    [DomName("HTMLDetailsElement")]
    public sealed class HTMLDetailsElement : HTMLElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML details element.
        /// </summary>
        internal HTMLDetailsElement()
        {
            _name = Tags.Details;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the details element is open.
        /// </summary>
        [DomName("open")]
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
