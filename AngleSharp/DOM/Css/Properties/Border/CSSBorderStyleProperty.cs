namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-style
    /// </summary>
    sealed class CssBorderStyleProperty : CssShorthandProperty, ICssBorderStylesProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>> Converter = 
            CssBorderPartStyleProperty.Converter.Val().Periodic();
        readonly CssBorderTopStyleProperty _top;
        readonly CssBorderRightStyleProperty _right;
        readonly CssBorderBottomStyleProperty _bottom;
        readonly CssBorderLeftStyleProperty _left;

        #endregion

        #region ctor

        internal CssBorderStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderStyle, rule)
        {
            _top = Get<CssBorderTopStyleProperty>();
            _right = Get<CssBorderRightStyleProperty>();
            _bottom = Get<CssBorderBottomStyleProperty>();
            _left = Get<CssBorderLeftStyleProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the style of the top border.
        /// </summary>
        public LineStyle Top
        {
            get { return _top.Style; }
        }

        /// <summary>
        /// Gets the value for the style of the right border.
        /// </summary>
        public LineStyle Right
        {
            get { return _right.Style; }
        }

        /// <summary>
        /// Gets the value for the style of the bottom border.
        /// </summary>
        public LineStyle Bottom
        {
            get { return _bottom.Style; }
        }

        /// <summary>
        /// Gets the value for the style of the left border.
        /// </summary>
        public LineStyle Left
        {
            get { return _left.Style; }
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
