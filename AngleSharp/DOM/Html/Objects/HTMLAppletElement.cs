using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML applet element.
    /// </summary>
    [DOM("HTMLAppletElement")]
    public sealed class HTMLAppletElement : HTMLElement
    {
        /// <summary>
        /// The applet tag.
        /// </summary>
        internal const String Tag = "applet";

        internal HTMLAppletElement()
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
