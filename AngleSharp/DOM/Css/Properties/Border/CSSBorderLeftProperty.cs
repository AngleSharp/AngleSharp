namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left
    /// </summary>
    sealed class CSSBorderLeftProperty : CSSShorthandProperty, ICssBorderProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Length, LineStyle, Color>> Converter = CSSBorderProperty.Converter;
        readonly CSSBorderLeftColorProperty _color;
        readonly CSSBorderLeftStyleProperty _style;
        readonly CSSBorderLeftWidthProperty _width;

        #endregion

        #region ctor

        internal CSSBorderLeftProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderLeft, rule, PropertyFlags.Animatable)
        {
            _color = Get<CSSBorderLeftColorProperty>();
            _style = Get<CSSBorderLeftStyleProperty>();
            _width = Get<CSSBorderLeftWidthProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the given border property.
        /// </summary>
        public Length Width
        {
            get { return _width.Width; }
        }

        /// <summary>
        /// Gets the color of the given border property.
        /// </summary>
        public Color Color
        {
            get { return _color.Color; }
        }

        /// <summary>
        /// Gets the style of the given border property.
        /// </summary>
        public LineStyle Style
        {
            get { return _style.Style; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                _width.SetWidth(m.Item1);
                _style.SetStyle(m.Item2);
                _color.SetColor(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return String.Format("{0} {1} {2}", _width.SerializeValue(), _style.SerializeValue(), _color.SerializeValue());
        }

        #endregion
    }
}
