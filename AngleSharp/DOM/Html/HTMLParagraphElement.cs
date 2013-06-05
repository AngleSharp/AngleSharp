using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML paragraph element.
    /// </summary>
    public class HTMLParagraphElement : HTMLElement
    {
        /// <summary>
        /// The p tag.
        /// </summary>
        public const string Tag = "p";

        /// <summary>
        /// Creates a new HTML paragraph element.
        /// </summary>
        public HTMLParagraphElement()
        {
            NodeName = Tag;
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
