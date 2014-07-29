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
            : base(Tags.Embed, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        public String Source
        {
            get { return GetAttribute(AttributeNames.Src); }
            set { SetAttribute(AttributeNames.Src, value); }
        }

        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        public String DisplayWidth
        {
            get { return GetAttribute(AttributeNames.Width); }
            set { SetAttribute(AttributeNames.Width, value); }
        }

        public String DisplayHeight
        {
            get { return GetAttribute(AttributeNames.Height); }
            set { SetAttribute(AttributeNames.Height, value); }
        }

        #endregion
    }
}
