namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border
    /// </summary>
    sealed class CssBorderProperty : CssShorthandProperty, ICssBorderProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue>> Converter = Converters.WithAny(
            CssBorderPartWidthProperty.Converter.Val().Option(CssValue.Initial),
            CssBorderPartStyleProperty.Converter.Val().Option(CssValue.Initial),
            CssBorderPartColorProperty.Converter.Val().Option(CssValue.Initial));

        readonly CssBorderTopColorProperty _topColor;
        readonly CssBorderTopStyleProperty _topStyle;
        readonly CssBorderTopWidthProperty _topWidth;
        readonly CssBorderRightColorProperty _rightColor;
        readonly CssBorderRightStyleProperty _rightStyle;
        readonly CssBorderRightWidthProperty _rightWidth;
        readonly CssBorderBottomColorProperty _bottomColor;
        readonly CssBorderBottomStyleProperty _bottomStyle;
        readonly CssBorderBottomWidthProperty _bottomWidth;
        readonly CssBorderLeftColorProperty _leftColor;
        readonly CssBorderLeftStyleProperty _leftStyle;
        readonly CssBorderLeftWidthProperty _leftWidth;

        #endregion

        #region ctor

        internal CssBorderProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Border, rule, PropertyFlags.Animatable)
        {
            _topColor = Get<CssBorderTopColorProperty>();
            _topStyle = Get<CssBorderTopStyleProperty>();
            _topWidth = Get<CssBorderTopWidthProperty>();
            _rightColor = Get<CssBorderRightColorProperty>();
            _rightStyle = Get<CssBorderRightStyleProperty>();
            _rightWidth = Get<CssBorderRightWidthProperty>();
            _bottomColor = Get<CssBorderBottomColorProperty>();
            _bottomStyle = Get<CssBorderBottomStyleProperty>();
            _bottomWidth = Get<CssBorderBottomWidthProperty>();
            _leftColor = Get<CssBorderLeftColorProperty>();
            _leftStyle = Get<CssBorderLeftStyleProperty>();
            _leftWidth = Get<CssBorderLeftWidthProperty>();
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

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                _topWidth.TrySetValue(m.Item1);
                _topStyle.TrySetValue(m.Item2);
                _topColor.TrySetValue(m.Item3);
                _leftWidth.TrySetValue(m.Item1);
                _leftStyle.TrySetValue(m.Item2);
                _leftColor.TrySetValue(m.Item3);
                _rightWidth.TrySetValue(m.Item1);
                _rightStyle.TrySetValue(m.Item2);
                _rightColor.TrySetValue(m.Item3);
                _bottomWidth.TrySetValue(m.Item1);
                _bottomStyle.TrySetValue(m.Item2);
                _bottomColor.TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
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
