using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML html element.
    /// </summary>
    public sealed class HTMLHtmlElement : HTMLElement
    {
        /// <summary>
        /// The html tag.
        /// </summary>
        public const string Tag = "html";

        /// <summary>
        /// Creates a new HTML html tag.
        /// </summary>
        public HTMLHtmlElement()
        {
            _name = Tag;
        }

        protected internal override bool IsSpecial
        {
            get { return true; }
        }
    }
}
