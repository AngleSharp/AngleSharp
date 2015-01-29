namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom
    /// </summary>
    sealed class CssBorderBottomProperty : CssShorthandProperty, ICssBorderProperty
    {
        #region Fields

        readonly CssBorderBottomColorProperty _color;
        readonly CssBorderBottomStyleProperty _style;
        readonly CssBorderBottomWidthProperty _width;

        #endregion

        #region ctor

        internal CssBorderBottomProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderBottom, rule, PropertyFlags.Animatable)
        {
            _color = Get<CssBorderBottomColorProperty>();
            _style = Get<CssBorderBottomStyleProperty>();
            _width = Get<CssBorderBottomWidthProperty>();
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
            return CssBorderProperty.Converter.TryConvert(value, m =>
            {
                _width.TrySetValue(m.Item1);
                _style.TrySetValue(m.Item2);
                _color.TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return String.Format("{0} {1} {2}", _width.SerializeValue(), _style.SerializeValue(), _color.SerializeValue());
        }

        #endregion
    }
}
