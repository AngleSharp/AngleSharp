namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration
    /// </summary>
    sealed class CSSTextDecorationProperty : CSSShorthandProperty, ICssTextDecorationProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue>> Converter = WithAny(
            CSSTextDecorationColorProperty.Converter.Val().Option(),
            CSSTextDecorationStyleProperty.Converter.Val().Option(),
            CSSTextDecorationLineProperty.Converter.Val().Option()
        );

        readonly CSSTextDecorationColorProperty _color;
        readonly CSSTextDecorationLineProperty _line;
        readonly CSSTextDecorationStyleProperty _style;

        #endregion

        #region ctor

        internal CSSTextDecorationProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.TextDecoration, rule, PropertyFlags.Animatable)
        {
            _color = Get<CSSTextDecorationColorProperty>();
            _line = Get<CSSTextDecorationLineProperty>();
            _style = Get<CSSTextDecorationStyleProperty>();
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

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return String.Format("{0} {1} {2}", _line.SerializeValue(), _style.SerializeValue(), _color.SerializeValue());
        }

        #endregion
    }
}
