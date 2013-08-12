using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML basefont element.
    /// </summary>
    [DOM("HTMLBaseFontElement")]
    public sealed class HTMLBaseFontElement : HTMLElement
    {
        /// <summary>
        /// The basefont tag.
        /// </summary>
        internal const String Tag = "basefont";

        internal HTMLBaseFontElement()
        {
            _name = Tag;
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
