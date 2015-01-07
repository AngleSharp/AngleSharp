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
    sealed class CssBorderColorProperty : CssShorthandProperty, ICssBorderColorsProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>> Converter = 
            CssBorderPartColorProperty.Converter.Val().Periodic();
        readonly CssBorderTopColorProperty _top;
        readonly CssBorderRightColorProperty _right;
        readonly CssBorderBottomColorProperty _bottom;
        readonly CssBorderLeftColorProperty _left;

        #endregion

        #region ctor

        internal CssBorderColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderColor, rule, PropertyFlags.Hashless | PropertyFlags.Animatable)
        {
            _top = Get<CssBorderTopColorProperty>();
            _right = Get<CssBorderRightColorProperty>();
            _bottom = Get<CssBorderBottomColorProperty>();
            _left = Get<CssBorderLeftColorProperty>();
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
                _top.TrySetValue(m.Item1);
                _right.TrySetValue(m.Item2);
                _bottom.TrySetValue(m.Item3);
                _left.TrySetValue(m.Item4);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            return SerializePeriodic(_top, _right, _bottom, _left);
        }

        #endregion
    }
}
