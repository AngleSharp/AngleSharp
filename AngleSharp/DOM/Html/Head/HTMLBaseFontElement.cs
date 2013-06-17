using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML basefont element.
    /// </summary>
    public sealed class HTMLBaseFontElement : HTMLElement
    {
        /// <summary>
        /// The basefont tag.
        /// </summary>
        internal const string Tag = "basefont";

        internal HTMLBaseFontElement()
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
