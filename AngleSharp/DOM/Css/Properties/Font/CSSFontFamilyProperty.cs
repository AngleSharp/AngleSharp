namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-family
    /// </summary>
    sealed class CSSFontFamilyProperty : CSSProperty, ICssFontFamilyProperty
    {
        #region Fields

        readonly List<String> _families;

        #endregion

        #region ctor

        internal CSSFontFamilyProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.FontFamily, rule, PropertyFlags.Inherited)
        {
            _families = new List<String>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration over all font names.
        /// </summary>
        public IEnumerable<String> Families
        {
            get { return _families; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _families.Clear();
            _families.Add("Times New Roman");
        }

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
                _families.Add(value.ToFontFamily());
            }
            else if (value is CSSValueList)
                return SetFamilies((CSSValueList)value);
            else
                return false;

            return true;
        }

        Boolean SetFamilies(CSSValueList values)
        {
            var families = new List<String>();
            var items = values.ToList();

            foreach (var item in items)
            {
                var family = (item.Length == 1 ? item[0] : item).ToFontFamily();

                if (family == null)
                    return false;

                families.Add(family);
            }

            _families.Clear();
            _families.AddRange(families);
            return true;
        }

        #endregion

        #region Modes

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
