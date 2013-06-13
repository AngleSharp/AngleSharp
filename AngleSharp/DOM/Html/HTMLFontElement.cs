using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a font element.
    /// </summary>
    public sealed class HTMLFontElement : HTMLFormattingElement
    {
        /// <summary>
        /// The font tag.
        /// </summary>
        internal const string Tag = "font";

        internal HTMLFontElement()
        {
            _name = Tag;
        }
    }
}
