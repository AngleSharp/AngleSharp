namespace AngleSharp.DOM.Css
{
    using System;

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
            CSSValue width = null;
            CSSValue color = null;
            CSSValue style = null;

            if (list.Length > 3)
                return false;

            for (int i = 0; i < list.Length; i++)
            {
                if (!_topWidth.CanStore(list[i], ref width) &&
                    !_topColor.CanStore(list[i], ref color) &&
                    !_topStyle.CanStore(list[i], ref style))
                    return false;
            }

            return _topWidth.TrySetValue(width) && _topColor.TrySetValue(color) && _topStyle.TrySetValue(style) &&
                   _leftWidth.TrySetValue(width) && _leftColor.TrySetValue(color) && _leftStyle.TrySetValue(style) &&
                   _rightWidth.TrySetValue(width) && _rightColor.TrySetValue(color) && _rightStyle.TrySetValue(style) &&
                   _bottomWidth.TrySetValue(width) && _bottomColor.TrySetValue(color) && _bottomStyle.TrySetValue(style);
        }

        #endregion
    }
}
