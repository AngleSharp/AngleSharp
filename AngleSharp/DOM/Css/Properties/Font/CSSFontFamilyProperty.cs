namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-family
    /// </summary>
    sealed class CSSFontFamilyProperty : CSSProperty, ICssFontFamilyProperty
    {
        #region Fields

        static readonly Dictionary<String, SystemFontFamily> defaultfamilies = new Dictionary<String, SystemFontFamily>(StringComparer.OrdinalIgnoreCase);
        List<BaseFontFamily> _families;

        #endregion

        #region ctor

        static CSSFontFamilyProperty()
        {
            defaultfamilies.Add(Keywords.Serif, new SystemFontFamily(SystemFonts.Serif));
            defaultfamilies.Add(Keywords.SansSerif, new SystemFontFamily(SystemFonts.SansSerif));
            defaultfamilies.Add(Keywords.Monospace, new SystemFontFamily(SystemFonts.Monospace));
            defaultfamilies.Add(Keywords.Cursive, new SystemFontFamily(SystemFonts.Cursive));
            defaultfamilies.Add(Keywords.Fantasy, new SystemFontFamily(SystemFonts.Fantasy));
        }

        internal CSSFontFamilyProperty()
            : base(PropertyNames.FontFamily, PropertyFlags.Inherited)
        {
            _families = new List<BaseFontFamily>();
            _families.Add(defaultfamilies[Keywords.Serif]);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration over all font names.
        /// </summary>
        public IEnumerable<String> Families
        {
            get 
            {
                foreach (var family in _families)
                    yield return family.FontName;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSPrimitiveValue)
            {
                _families.Clear();
                _families.Add(GetFamily(value));
            }
            else if (value is CSSValueList)
                return SetFamilies((CSSValueList)value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        Boolean SetFamilies(CSSValueList values)
        {
            var families = new List<BaseFontFamily>();
            var items = values.ToList();

            foreach (var item in items)
            {
                var family = GetFamily(item.Length == 1 ? item[0] : item);

                if (family == null)
                    return false;

                families.Add(family);
            }

            _families = families;
            return true;
        }

        static BaseFontFamily GetFamily(CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
            {
                if (primitive.Unit == UnitType.Ident)
                {
                    SystemFontFamily family;
                    var name = primitive.GetString();

                    if (defaultfamilies.TryGetValue(name, out family))
                        return family;

                    return new CustomFontFamily(name);
                }
                else if (primitive.Unit == UnitType.String)
                {
                    return new CustomFontFamily(primitive.GetString());
                }
            }
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;
                var names = new String[values.Length];

                for (var i = 0; i < names.Length; i++)
                {
                    var ident = values[i] as CSSPrimitiveValue;

                    if (ident == null || ident.Unit != UnitType.Ident)
                        return null;

                    names[i] = ident.GetString();
                }

                return new CustomFontFamily(String.Join(" ", names));
            }

            return null;
        }

        #endregion

        #region Modes

        abstract class BaseFontFamily
        {
            protected String _name;

            public String FontName
            {
                get { return _name; }
            }
        }

        sealed class CustomFontFamily : BaseFontFamily
        {
            public CustomFontFamily(String familyName)
            {
                _name = familyName;
            }
        }

        sealed class SystemFontFamily : BaseFontFamily
        {
            public SystemFontFamily(SystemFonts font)
            {
                _name = font.ToString();
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
