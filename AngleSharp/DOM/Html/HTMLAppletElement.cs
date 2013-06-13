using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML applet element.
    /// </summary>
    public sealed class HTMLAppletElement : HTMLElement
    {
        /// <summary>
        /// The applet tag.
        /// </summary>
        internal const string Tag = "applet";

        internal HTMLAppletElement()
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
