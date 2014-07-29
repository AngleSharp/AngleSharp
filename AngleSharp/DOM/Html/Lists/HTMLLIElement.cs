namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents an HTML li, dd or dt tag.
    /// </summary>
    sealed class HTMLLIElement : HTMLElement, IImpliedEnd, IHtmlListItemElement
    {
        #region ctor

        /// <summary>
        /// Creates a new item tag.
        /// </summary>
        internal HTMLLIElement(String name)
            : base(name, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        public Int32? Value
        {
            get { var i = 0; return Int32.TryParse(GetAttribute(AttributeNames.Value), out i) ? i : new Int32?(); }
            set { SetAttribute(AttributeNames.Value, value.HasValue ? value.Value.ToString() : null); }
        }

        #endregion
    }
}
