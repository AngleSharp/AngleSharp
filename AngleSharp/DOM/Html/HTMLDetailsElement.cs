using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML details element.
    /// </summary>
    public sealed class HTMLDetailsElement : HTMLElement
    {
        /// <summary>
        /// The details tag.
        /// </summary>
        public const string Tag = "details";

        /// <summary>
        /// Creates a new HTML details element.
        /// </summary>
        public HTMLDetailsElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets or sets if the details element is open.
        /// </summary>
        public bool Open
        {
            get { return GetAttribute("open") != null; }
            set { SetAttribute("open", value ? string.Empty : null); }
        }

        protected internal override bool IsSpecial
        {
            get { return true; }
        }
    }
}
