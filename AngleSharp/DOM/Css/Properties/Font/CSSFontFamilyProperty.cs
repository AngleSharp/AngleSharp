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

        static readonly Dictionary<String, SystemFontFamily> defaultfamilies = new Dictionary<String, SystemFontFamily>(StringComparer.OrdinalIgnoreCase);
        List<BaseFontFamily> _families;

        #endregion

        #region ctor

        static CSSFontFamilyProperty()
        {
            defaultfamilies.Add("serif", new SystemFontFamily(SystemFonts.Serif));
            defaultfamilies.Add("sans-serif", new SystemFontFamily(SystemFonts.SansSerif));
            defaultfamilies.Add("monospace", new SystemFontFamily(SystemFonts.Monospace));
            defaultfamilies.Add("cursive", new SystemFontFamily(SystemFonts.Cursive));
            defaultfamilies.Add("fantasy", new SystemFontFamily(SystemFonts.Fantasy));
        }

        internal CSSFontFamilyProperty()
            : base(PropertyNames.FontFamily, PropertyFlags.Inherited)
        {
            _families = new List<BaseFontFamily>();
            _families.Add(defaultfamilies["serif"]);
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

        static CustomFontFamily FromIdentifiers(CSSValue value)
        {
            if (value is CSSIdentifierValue)
                return new CustomFontFamily(((CSSIdentifierValue)value).Value);
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

                return new CustomFontFamily(String.Join(" ", content));
            }

            return null;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue || value is CSSStringValue)
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
            if (value is CSSIdentifierValue)
            {
                SystemFontFamily family;
                var name = ((CSSIdentifierValue)value).Value;

                if (defaultfamilies.TryGetValue(name, out family))
                    return family;

                return new CustomFontFamily(name);
            }
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;
                var names = new String[values.Length];

                for (var i = 0; i < names.Length; i++)
                {
                    var ident = values[i] as CSSIdentifierValue;

                    if (ident == null)
                        return null;

                    names[i] = ident.Value;
                }

                return new CustomFontFamily(String.Join(" ", names));
            }
            else if (value is CSSStringValue)
                return new CustomFontFamily(((CSSStringValue)value).Value);

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
