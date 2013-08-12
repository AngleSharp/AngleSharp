using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the title element.
    /// </summary>
    [DOM("HTMLTitleElement")]
    public sealed class HTMLTitleElement : HTMLElement
    {
        /// <summary>
        /// The title tag.
        /// </summary>
        internal const String Tag = "title";

        /// <summary>
        /// Creates a new HTML title element.
        /// </summary>
        internal HTMLTitleElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets or sets the text of the title.
        /// </summary>
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
