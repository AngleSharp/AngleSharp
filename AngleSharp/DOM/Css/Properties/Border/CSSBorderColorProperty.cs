namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-color
    /// </summary>
    sealed class CSSBorderColorProperty : CSSShorthandProperty, ICssBorderColorsProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Color, Color, Color, Color>> Converter = CSSBorderPartColorProperty.Converter.Periodic();
        readonly CSSBorderTopColorProperty _top;
        readonly CSSBorderRightColorProperty _right;
        readonly CSSBorderBottomColorProperty _bottom;
        readonly CSSBorderLeftColorProperty _left;

        #endregion

        #region ctor

        internal CSSBorderColorProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderColor, rule, PropertyFlags.Hashless | PropertyFlags.Animatable)
        {
            _top = Get<CSSBorderTopColorProperty>();
            _right = Get<CSSBorderRightColorProperty>();
            _bottom = Get<CSSBorderBottomColorProperty>();
            _left = Get<CSSBorderLeftColorProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the color of the top border.
        /// </summary>
        public Color Top
        {
            get { return _top.Color; }
        }

        /// <summary>
        /// Gets the value for the color of the right border.
        /// </summary>
        public Color Right
        {
            get { return _right.Color; }
        }

        /// <summary>
        /// Gets the value for the color of the bottom border.
        /// </summary>
        public Color Bottom
        {
            get { return _bottom.Color; }
        }

        /// <summary>
        /// Gets the value for the color of the left border.
        /// </summary>
        public Color Left
        {
            get { return _left.Color; }
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
                _top.SetColor(m.Item1);
                _right.SetColor(m.Item2);
                _bottom.SetColor(m.Item3);
                _left.SetColor(m.Item4);
            });
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return SerializePeriodic(_top, _right, _bottom, _left);
        }

        #endregion
    }
}
