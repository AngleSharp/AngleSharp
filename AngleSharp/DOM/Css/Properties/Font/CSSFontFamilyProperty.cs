namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-family
    /// </summary>
    sealed class CSSFontFamilyProperty : CSSProperty, ICssFontFamilyProperty
    {
        #region Fields

        internal static readonly String[] Default = new [] { "Times New Roman" };
        internal static readonly IValueConverter<String[]> Converter = TakeList(WithFontFamily());
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

        private void SetFamilies(IEnumerable<String> families)
        {
            _families.Clear();
            _families.AddRange(families);
        }

        internal override void Reset()
        {
            _families.Clear();
            _families.AddRange(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, SetFamilies);
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
