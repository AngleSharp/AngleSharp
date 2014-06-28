namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the embed element.
    /// </summary>
    sealed class HTMLEmbedElement : HTMLElement, IHtmlEmbedElement
    {
        #region ctor

        /// <summary>
        /// Creates a new Embed element.
        /// </summary>
        internal HTMLEmbedElement()
        {
            _name = Tags.Embed;
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

        #region Properties

        public String Src
        {
            get { return GetAttribute(AttributeNames.Src); }
            set { SetAttribute(AttributeNames.Src, value); }
        }

        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        public String Width
        {
            get { return GetAttribute(AttributeNames.Width); }
            set { SetAttribute(AttributeNames.Width, value); }
        }

        public String Height
        {
            get { return GetAttribute(AttributeNames.Height); }
            set { SetAttribute(AttributeNames.Height, value); }
        }

        #endregion
    }
}
