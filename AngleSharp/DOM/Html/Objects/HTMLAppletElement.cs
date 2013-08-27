using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML applet element.
    /// </summary>
    [DOM("HTMLAppletElement")]
    public sealed class HTMLAppletElement : HTMLElement
    {
        internal HTMLAppletElement()
        {
            _name = Tags.APPLET;
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
