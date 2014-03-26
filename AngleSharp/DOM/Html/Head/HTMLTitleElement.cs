namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the title element.
    /// </summary>
    [DOM("HTMLTitleElement")]
    public sealed class HTMLTitleElement : HTMLElement
    {
        /// <summary>
        /// Creates a new HTML title element.
        /// </summary>
        internal HTMLTitleElement()
        {
            _name = Tags.TITLE;
        }

        /// <summary>
        /// Gets or sets the text of the title.
        /// </summary>
        [DOM("text")]
        public String Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }
    }
}
