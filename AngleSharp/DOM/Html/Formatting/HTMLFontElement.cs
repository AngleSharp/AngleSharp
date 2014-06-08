using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a font element.
    /// </summary>
    [DomName("HTMLFontElement")]
    public sealed class HTMLFontElement : HTMLElement, IFormatting
    {
        internal const String AttrColor = "color";

        internal const String AttrFace = "face";

        internal const String AttrSize = "size";

        internal HTMLFontElement()
        {
            _name = Tags.Font;
        }
    }
}
