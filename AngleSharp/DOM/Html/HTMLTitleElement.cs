using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the title element.
    /// </summary>
    public class HTMLTitleElement : HTMLRCDataElement
    {
        /// <summary>
        /// The title tag.
        /// </summary>
        public const string Tag = "title";

        /// <summary>
        /// Creates a new HTML title element.
        /// </summary>
        public HTMLTitleElement()
        {
            NodeName = Tag;
        }

        /// <summary>
        /// Gets or sets the text of the title.
        /// </summary>
        public string Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        protected internal override bool IsSpecial
        {
            get
            {
                return true;
            }
        }
    }
}
