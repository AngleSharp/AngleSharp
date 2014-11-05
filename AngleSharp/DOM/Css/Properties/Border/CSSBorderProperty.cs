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
            var list = value as CSSValueList ?? new CSSValueList(value);
            CSSValue width = null, color = null, style = null;

            if (list.Length > 3)
                return false;

            for (int i = 0; i < list.Length; i++)
            {
                if (!_topWidth.CanStore(list[i], ref width) && !_topStyle.CanStore(list[i], ref style) && !_topColor.CanStore(list[i], ref color))
                    return false;
            }

            return _topWidth.TrySetValue(width) && _topColor.TrySetValue(color) && _topStyle.TrySetValue(style) &&
                   _leftWidth.TrySetValue(width) && _leftColor.TrySetValue(color) && _leftStyle.TrySetValue(style) &&
                   _rightWidth.TrySetValue(width) && _rightColor.TrySetValue(color) && _rightStyle.TrySetValue(style) &&
                   _bottomWidth.TrySetValue(width) && _bottomColor.TrySetValue(color) && _bottomStyle.TrySetValue(style);
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
