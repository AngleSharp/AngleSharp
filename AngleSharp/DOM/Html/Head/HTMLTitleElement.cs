using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the title element.
    /// </summary>
    public sealed class HTMLTitleElement : HTMLElement
    {
        /// <summary>
        /// The title tag.
        /// </summary>
        internal const string Tag = "title";

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
        public string Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }
    }
}
