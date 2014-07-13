using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a font element.
    /// </summary>
    sealed class HTMLFontElement : HTMLElement, IFormatting
    {
        internal HTMLFontElement()
        {
            _name = Tags.Font;
        }
    }
}
