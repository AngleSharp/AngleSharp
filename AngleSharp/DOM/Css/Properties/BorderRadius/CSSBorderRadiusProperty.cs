namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius
    /// </summary>
    sealed class CSSBorderRadiusProperty : CSSShorthandProperty, ICssBorderRadiusProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<IDistance, IDistance, IDistance, IDistance>[]> Converter = 
            CSSBorderRadiusPartProperty.SingleConverter.Periodic().OptionalSplit().Constraint(m => m != null && m.Length < 3);

        readonly CSSBorderTopLeftRadiusProperty _topLeft;
        readonly CSSBorderTopRightRadiusProperty _topRight;
        readonly CSSBorderBottomRightRadiusProperty _bottomRight;
        readonly CSSBorderBottomLeftRadiusProperty _bottomLeft;

        #endregion

        #region ctor

        internal CSSBorderRadiusProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderRadius, rule, PropertyFlags.Animatable)
        {
            _topLeft = Get<CSSBorderTopLeftRadiusProperty>();
            _topRight = Get<CSSBorderTopRightRadiusProperty>();
            _bottomRight = Get<CSSBorderBottomRightRadiusProperty>();
            _bottomLeft = Get<CSSBorderBottomLeftRadiusProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the horizontal bottom-left radius.
        /// </summary>
        public IDistance HorizontalBottomLeft
        {
            get { return _bottomLeft.HorizontalRadius; }
        }

        /// <summary>
        /// Gets the value of the vertical bottom-left radius.
        /// </summary>
        public IDistance VerticalBottomLeft
        {
            get { return _bottomLeft.VerticalRadius; }
        }

        /// <summary>
        /// Gets the value of the horizontal bottom-right radius.
        /// </summary>
        public IDistance HorizontalBottomRight
        {
            get { return _bottomRight.HorizontalRadius; }
        }

        /// <summary>
        /// Gets the value of the vertical bottom-right radius.
        /// </summary>
        public IDistance VerticalBottomRight
        {
            get { return _bottomRight.VerticalRadius; }
        }

        /// <summary>
        /// Gets the value of the horizontal top-left radius.
        /// </summary>
        public IDistance HorizontalTopLeft
        {
            get { return _topLeft.HorizontalRadius; }
        }

        /// <summary>
        /// Gets the value of the vertical top-left radius.
        /// </summary>
        public IDistance VerticalTopLeft
        {
            get { return _topLeft.VerticalRadius; }
        }

        /// <summary>
        /// Gets the value of the horizontal top-right radius.
        /// </summary>
        public IDistance HorizontalTopRight
        {
            get { return _topRight.HorizontalRadius; }
        }

        /// <summary>
        /// Gets the value of the vertical top-right radius.
        /// </summary>
        public IDistance VerticalTopRight
        {
            get { return _topRight.VerticalRadius; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="v">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                var elliptic = m.Length == 2;
                var h = m[0];
                var v = m[elliptic ? 1 : 0];
                _topLeft.SetRadius(h.Item1, v.Item1);
                _topRight.SetRadius(h.Item2, v.Item2);
                _bottomRight.SetRadius(h.Item3, v.Item3);
                _bottomLeft.SetRadius(h.Item4, v.Item4);
            });
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            var horizontal = SerializePeriodic(_topLeft.HorizontalRadius, _topRight.HorizontalRadius, _bottomRight.HorizontalRadius, _bottomLeft.HorizontalRadius);

            if (_topLeft.IsCircle && _topRight.IsCircle && _bottomRight.IsCircle && _bottomLeft.IsCircle)
                return horizontal;

            var vertical = SerializePeriodic(_topLeft.VerticalRadius, _topRight.VerticalRadius, _bottomRight.VerticalRadius, _bottomLeft.VerticalRadius);
            return horizontal + " / " + vertical;
        }

        #endregion
    }
}
