namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration
    /// </summary>
    sealed class CssTextDecorationProperty : CssShorthandProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue>> Converter = Converters.WithAny(
            CssTextDecorationColorProperty.Converter.Val().Option(),
            CssTextDecorationStyleProperty.Converter.Val().Option(),
            CssTextDecorationLineProperty.Converter.Val().Option()
        );

        readonly CssTextDecorationColorProperty _color;
        readonly CssTextDecorationLineProperty _line;
        readonly CssTextDecorationStyleProperty _style;

        #endregion

        #region ctor

        internal CssTextDecorationProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TextDecoration, rule, PropertyFlags.Animatable)
        {
            _color = Get<CssTextDecorationColorProperty>();
            _line = Get<CssTextDecorationLineProperty>();
            _style = Get<CssTextDecorationStyleProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the decoration style property.
        /// </summary>
        public TextDecorationStyle DecorationStyle
        {
            get { return _style.DecorationStyle; }
        }

        /// <summary>
        /// Gets the value of the line property.
        /// </summary>
        public IEnumerable<TextDecorationLine> Lines
        {
            get { return _line.Lines; }
        }

        /// <summary>
        /// Gets the value of the color property.
        /// </summary>
        public Color Color
        {
            get { return _color.Color; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                _color.TrySetValue(m.Item1);
                _style.TrySetValue(m.Item2);
                _line.TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return String.Format("{0} {1} {2}", _line.SerializeValue(), _style.SerializeValue(), _color.SerializeValue());
        }

        #endregion
    }
}
