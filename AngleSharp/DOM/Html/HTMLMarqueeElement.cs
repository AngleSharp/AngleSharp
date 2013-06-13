using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML marquee element.
    /// </summary>
    sealed class HTMLMarqueeElement : HTMLElement
    {
        /// <summary>
        /// The marquee tag.
        /// </summary>
        internal const string Tag = "marquee";

        internal HTMLMarqueeElement()
        {
            _name = Tag;
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
