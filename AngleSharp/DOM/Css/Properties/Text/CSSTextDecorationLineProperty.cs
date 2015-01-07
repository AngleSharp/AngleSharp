namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-line
    /// </summary>
    sealed class CssTextDecorationLineProperty : CssProperty, ICssTextDecorationLineProperty
    {
        #region Fields

        internal static readonly TextDecorationLine[] Default = new TextDecorationLine[0];
        internal static readonly IValueConverter<TextDecorationLine[]> SingleConverter = Map.TextDecorationLines.ToConverter().Many();
        internal static readonly IValueConverter<TextDecorationLine[]> Converter = SingleConverter.Or(Keywords.None, Default);
        readonly List<TextDecorationLine> _lines;

        #endregion

        #region ctor

        internal CssTextDecorationLineProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TextDecorationLine, rule)
        {
            _lines = new List<TextDecorationLine>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration over all selected styles
        /// for text decoration lines.
        /// </summary>
        public IEnumerable<TextDecorationLine> Lines
        {
            get { return _lines; }
        }

        #endregion

        #region Methods

        public void SetLines(IEnumerable<TextDecorationLine> lines)
        {
            _lines.Clear();
            _lines.AddRange(lines);
        }

        internal override void Reset()
        {
            _lines.Clear();
            _lines.AddRange(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetLines);
        }

        #endregion
    }
}
