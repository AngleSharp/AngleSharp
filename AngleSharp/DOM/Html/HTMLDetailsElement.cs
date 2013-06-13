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
        internal const string Tag = "details";

        /// <summary>
        /// Creates a new HTML details element.
        /// </summary>
        internal HTMLDetailsElement()
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

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }
    }
}
