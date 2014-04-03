namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-family
    /// </summary>
    public sealed class CSSFontFamilyProperty : CSSProperty
    {
        #region Fields

        static readonly ValueConverter<BaseFontFamily> _families = new ValueConverter<BaseFontFamily>();
        BaseFontFamily _family;

        #endregion

        #region ctor

        static CSSFontFamilyProperty()
        {
            _families.AddStatic("serif", new SystemFontFamily(SystemFonts.Serif));
            _families.AddStatic("sans-serif", new SystemFontFamily(SystemFonts.SansSerif));
            _families.AddStatic("monospace", new SystemFontFamily(SystemFonts.Monospace));
            _families.AddStatic("cursive", new SystemFontFamily(SystemFonts.Cursive));
            _families.AddStatic("fantasy", new SystemFontFamily(SystemFonts.Fantasy));
            _families.AddConstructed<ResolveFontFamily>();
            _families.AddDelegate(FromIdentifiers);
            _families.AddMultiple<AlternativeFontFamily>();
        }

        public CSSFontFamilyProperty()
            : base(PropertyNames.FontFamily)
        {
            _family = _families.GetStatic("serif");
            _inherited = true;
        }

        #endregion

        #region Methods

        static ResolveFontFamily FromIdentifiers(CSSValue value)
        {
            if (value is CSSIdentifierValue)
                return new ResolveFontFamily(((CSSIdentifierValue)value).Value);
            else if (value is CSSValueList)
            {
                var list = (CSSValueList)value;
                var content = new String[list.Length];

                for (var i = 0; i < list.Length; i++)
                {
                    if (list[i] is CSSIdentifierValue)
                        content[i] = ((CSSIdentifierValue)list[i]).Value;
                    else
                        return null;
                }

                return new ResolveFontFamily(String.Join(" ", content));
            }

            return null;
        }

        protected override Boolean IsValid(CSSValue value)
        {
            BaseFontFamily family;

            if (_families.TryCreate(value, out family))
                _family = family;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class BaseFontFamily
        {
        }

        sealed class ResolveFontFamily : BaseFontFamily
        {
            String _familyName;

            public ResolveFontFamily(String familyName)
            {
                _familyName = familyName;
            }
        }

        sealed class AlternativeFontFamily : BaseFontFamily
        {
            List<BaseFontFamily> _fonts;

            public AlternativeFontFamily(List<BaseFontFamily> fonts)
            {
                _fonts = fonts;
            }
        }

        sealed class SystemFontFamily : BaseFontFamily
        {
            public SystemFontFamily(SystemFonts font)
            {
                //TODO select appropriate font
            }
        }

        enum SystemFonts
        {
            /// <summary>
            /// Glyphs have finishing strokes, flared or tapering ends, or have actual serifed endings.
            /// E.g.  Palatino, "Palatino Linotype", Palladio, "URW Palladio", serif
            /// </summary>
            Serif,
            /// <summary>
            /// Glyphs have stroke endings that are plain.
            /// E.g. 'Trebuchet MS', 'Liberation Sans', 'Nimbus Sans L', sans-serif
            /// </summary>
            SansSerif,
            /// <summary>
            /// All glyphs have the same fixed width.
            /// E.g. "DejaVu Sans Mono", Menlo, Consolas, "Liberation Mono", Monaco, "Lucida Console", monospace
            /// </summary>
            Monospace,
            /// <summary>
            /// Glyphs in cursive fonts generally have either joining strokes or other cursive characteristics
            /// beyond those of italic typefaces. The glyphs are partially or completely connected, and the
            /// result looks more like handwritten pen or brush writing than printed letterwork.
            /// </summary>
            Cursive,
            /// <summary>
            /// Fantasy fonts are primarily decorative fonts that contain playful representations of characters.
            /// </summary>
            Fantasy
        }

        #endregion
    }
}
