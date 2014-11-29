namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border
    /// </summary>
    sealed class CSSBorderProperty : CSSShorthandProperty, ICssBorderProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Length, LineStyle, Color>> Converter = WithOptions(
            CSSBorderPartWidthProperty.Converter,
            CSSBorderPartStyleProperty.Converter,
            CSSBorderPartColorProperty.Converter,
            Tuple.Create(CSSBorderPartWidthProperty.Default, CSSBorderPartStyleProperty.Default, CSSBorderPartColorProperty.Default));

        readonly CSSBorderTopColorProperty _topColor;
        readonly CSSBorderTopStyleProperty _topStyle;
        readonly CSSBorderTopWidthProperty _topWidth;
        readonly CSSBorderRightColorProperty _rightColor;
        readonly CSSBorderRightStyleProperty _rightStyle;
        readonly CSSBorderRightWidthProperty _rightWidth;
        readonly CSSBorderBottomColorProperty _bottomColor;
        readonly CSSBorderBottomStyleProperty _bottomStyle;
        readonly CSSBorderBottomWidthProperty _bottomWidth;
        readonly CSSBorderLeftColorProperty _leftColor;
        readonly CSSBorderLeftStyleProperty _leftStyle;
        readonly CSSBorderLeftWidthProperty _leftWidth;

        #endregion

        #region ctor

        internal CSSBorderProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Border, rule, PropertyFlags.Animatable)
        {
            _topColor = Get<CSSBorderTopColorProperty>();
            _topStyle = Get<CSSBorderTopStyleProperty>();
            _topWidth = Get<CSSBorderTopWidthProperty>();
            _rightColor = Get<CSSBorderRightColorProperty>();
            _rightStyle = Get<CSSBorderRightStyleProperty>();
            _rightWidth = Get<CSSBorderRightWidthProperty>();
            _bottomColor = Get<CSSBorderBottomColorProperty>();
            _bottomStyle = Get<CSSBorderBottomStyleProperty>();
            _bottomWidth = Get<CSSBorderBottomWidthProperty>();
            _leftColor = Get<CSSBorderLeftColorProperty>();
            _leftStyle = Get<CSSBorderLeftStyleProperty>();
            _leftWidth = Get<CSSBorderLeftWidthProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the given border property.
        /// </summary>
        public Length Width
        {
            get { return _leftWidth.Width; }
        }

        /// <summary>
        /// Gets the color of the given border property.
        /// </summary>
        public Color Color
        {
            get { return _leftColor.Color; }
        }

        /// <summary>
        /// Gets the style of the given border property.
        /// </summary>
        public LineStyle Style
        {
            get { return _leftStyle.Style; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                _topWidth.SetWidth(m.Item1);
                _topStyle.SetStyle(m.Item2);
                _topColor.SetColor(m.Item3);
                _leftWidth.SetWidth(m.Item1);
                _leftStyle.SetStyle(m.Item2);
                _leftColor.SetColor(m.Item3);
                _rightWidth.SetWidth(m.Item1);
                _rightStyle.SetStyle(m.Item2);
                _rightColor.SetColor(m.Item3);
                _bottomWidth.SetWidth(m.Item1);
                _bottomStyle.SetStyle(m.Item2);
                _bottomColor.SetColor(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            var values = new List<String>();
            var width = _leftWidth.SerializeValue();
            var style = _leftStyle.SerializeValue();
            var color = _leftColor.SerializeValue();

            if (width == _topWidth.SerializeValue() && width == _bottomWidth.SerializeValue() && width == _rightWidth.SerializeValue())
                values.Add(width);

            if (style == _topWidth.SerializeValue() && style == _bottomWidth.SerializeValue() && style == _rightWidth.SerializeValue())
                values.Add(style);

            if (color == _topWidth.SerializeValue() && color == _bottomWidth.SerializeValue() && color == _rightWidth.SerializeValue())
                values.Add(color);

            return String.Join(" ", values);
        }

        #endregion
    }
}
